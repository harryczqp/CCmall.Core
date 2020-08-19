using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Autofac;
using Autofac.Extras.DynamicProxy;
using CCmall.Common.Configurations;
using CCmall.Core.Api.Extensions;
using CCmall.Core.Api.Filters;
using CCmall.Core.Common.Hubs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using NLog;
using CCmall.Common.Redis;
using CCmall.Common.Consul;
using Microsoft.Extensions.Options;
using System.Net;
using System.Linq;

namespace CCmall.Core.Api
{
    public class Startup
    {
        private static readonly ILogger _logger = LogManager.GetCurrentClassLogger();
        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Env { get; }
        private string _serviceAddress=string.Empty;
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            Env = env;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var ip = Dns.GetHostAddresses(Dns.GetHostName()).LastOrDefault();
            if (ip != null)
            {
                var port = Configuration.GetSection("URLS").Get<Uri>().Port;
                _serviceAddress = $"http://{ip}:{port}";
            }
            services.AddSingleton(new Appsettings(Env.ContentRootPath));
            services.AddSingleton<IRedisManager, RedisManager>();
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
            services.AddSignalR();
            //consul
            services.AddHealthChecks();
            
            services.AddConsul();
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            var basePath = Microsoft.DotNet.PlatformAbstractions.ApplicationEnvironment.ApplicationBasePath;
            try
            {
                var interpectors = new List<Type>();
                if (Appsettings.AppConfig.CCmallLogAop.Enable)
                {
                    builder.RegisterType<CCmallLogAop>();
                    interpectors.Add(typeof(CCmallLogAop));
                }
                //注入Respository
                var respositoryFilePath = Path.Combine(basePath, "CCmall.Repository.dll");
                var assemblyRespository = Assembly.LoadFrom(respositoryFilePath);
                builder.RegisterAssemblyTypes(assemblyRespository)
                    .AsImplementedInterfaces()
                    .InstancePerDependency()
                    .EnableInterfaceInterceptors()
                    .InterceptedBy(interpectors.ToArray());
            }
            catch (Exception ex)
            {
                _logger.Error($"Respository.dll异常{ex.Message}");
            }

        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IOptions<ConsulServiceOptions> options)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }
            
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
            //swagger
            app.UseSwaggerSetup();

            app.UseCors("LimitRequests");
            app.UseHealthChecks(options.Value.HealthCheck);

            app.UseConsul(_serviceAddress);
            //TODO 查资料UseEndpoints
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapHub<ChatHub>("/chatHub");
            });
        }
    }
}
