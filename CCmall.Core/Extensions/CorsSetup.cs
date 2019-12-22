using CCmall.Common;
using CCmall.Common.Configurations;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Blog.Core.Extensions
{
    /// <summary>
    /// Cors 启动服务
    /// </summary>
    public static class CorsSetup
    {
        public static void AddCorsSetup(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddCors(c =>
            {
                c.AddPolicy("LimitRequests", policy =>
                {
                    policy
                    .WithOrigins(Appsettings.Startup.Cors.IPs.Split(','))
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });
            });
        }
    }
}
