using System;
using System.IO;
using System.Reflection;
using Autofac;
using CCmall.Common.Configurations;
using CCmall.Core.Api.Extensions;
using CCmall.Core.Api.Filters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using NLog;

namespace CCmall.Core.Api
{
    public class Startup
    {
        private static readonly ILogger _logger = LogManager.GetCurrentClassLogger();
        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Env { get; }
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            Env = env;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(new Appsettings(Env.ContentRootPath));
            services.AddSwaggerSetup();
            services.AddAuthorizationSetup();
            services.AddSqlsugarSetup();
            services.AddCorsSetup();
            services.AddControllers(o =>
            {
                o.Filters.Add<ResultFilter>();
                o.Filters.Add<ExceptionFilter>();
                //TODO 全局异常过滤
            })
            .AddNewtonsoftJson(options =>
            {
                //忽略循环引用
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                //不使用驼峰样式的key
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
            });
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            var basePath = Microsoft.DotNet.PlatformAbstractions.ApplicationEnvironment.ApplicationBasePath;
            try
            {
                //注入Services
                var servicesFilePath = Path.Combine(basePath, "CCmall.Services.dll");
                var assemblyServices = Assembly.LoadFrom(servicesFilePath);
                builder.RegisterAssemblyTypes(assemblyServices)
                    .AsImplementedInterfaces()
                    .InstancePerDependency();
                //注入Respository
                var respositoryFilePath = Path.Combine(basePath, "CCmall.Repository.dll");
                var assemblyRespository = Assembly.LoadFrom(respositoryFilePath);
                builder.RegisterAssemblyTypes(assemblyRespository)
                    .AsImplementedInterfaces()
                    .InstancePerDependency();
            }
            catch (Exception ex)
            {
                _logger.Error($"Services.dll、Respository.dll异常{ex.Message}");
            }

        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseSwagger();

            app.UseSwaggerUI(options =>
            {
                var ApiName = Appsettings.Startup.ApiName;
                var Version = Appsettings.Startup.ApiVersion;
                options.SwaggerEndpoint($"/swagger/{Version}/swagger.json", $"{Version}");
                options.RoutePrefix = "";
            });
            // 使用静态文件
            app.UseStaticFiles();
            // 使用cookie
            app.UseCookiePolicy();
            // 返回错误码
            app.UseStatusCodePages();//把错误码返回前台，比如是404
            // Routing
            app.UseRouting();
            // 认证
            app.UseAuthentication();
            // 授权中间件
            app.UseAuthorization();

            app.UseCors("LimitRequests");

            //TODO 查资料UseEndpoints
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

            });
        }
    }
}
