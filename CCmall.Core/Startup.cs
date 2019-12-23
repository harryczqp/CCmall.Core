using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using CCmall.Common;
using CCmall.Common.Configurations;
using CCmall.Core.Extensions;
using CCmall.Core.Filters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CCmall.Core
{
    public class Startup
    {
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
            services.AddControllers(o =>
            {
                o.Filters.Add<ResultFilter>();
                o.Filters.Add<ExceptionFilter>(); 
                //TODO 全局异常过滤
            })
            .AddNewtonsoftJson(options =>
            {
                //TODO 全局配置Json序列化处理
            });
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {

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

            app.UseSwaggerUI(options=>
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
