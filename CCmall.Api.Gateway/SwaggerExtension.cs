using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CCmall.ApiGateWay.SwaggerExtension
{
    public static class SwaggerExtension
    {
        public static void AddSwaggerExtension(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("ApiGateWay", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "网关服务"
                });
            });
        }

        public static void UseSwaggerExtension(this IApplicationBuilder app)
        {
            app.UseSwagger()
               .UseSwaggerUI(options =>
               {
                   options.SwaggerEndpoint($"/api/CCmall.Core/swagger.json", "api");
                   options.RoutePrefix = "";
               });
        }
    }
}
