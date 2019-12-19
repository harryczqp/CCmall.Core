using System;
using System.IO;
using System.Linq;
using CCmall.Common;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using NLog;

namespace CCmall.Core.Extensions
{
    /// <summary>
    /// Swagger 启动服务
    /// </summary>
    public static class SwaggerSetup
    {
        private static readonly ILogger _logger = LogManager.GetCurrentClassLogger();
        public static void AddSwaggerSetup(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            var basePath = AppContext.BaseDirectory;
            var ApiName = Appsettings.app(new[] { "Startup", "ApiName" });
            var Version = Appsettings.app("Startup", "ApiVersion");
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(Version, new OpenApiInfo
                {
                    Version = Version,
                    Title = $"{ApiName} 接口文档",
                    Description = $"{ApiName} HTTP API " + Version,
                });
                c.OrderActionsBy(o => o.RelativePath);
                try
                {
                    var xmlPath = Path.Combine(basePath, "CCmall.Core.xml");
                    c.IncludeXmlComments(xmlPath, true);

                    var xmlModelPath = Path.Combine(basePath, "CCmall.Model.xml");
                    c.IncludeXmlComments(xmlModelPath);
                }
                catch (Exception ex)
                {
                    _logger.Error("CCmall.Core.xml和CCmall.Model.xml 丢失，请检查并拷贝。\n" + ex.Message);
                }
                c.OperationFilter<AddResponseHeadersFilter>();
                c.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();
                c.OperationFilter<SecurityRequirementsOperationFilter>();

                // Token绑定到ConfigureServices
                c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Description = "JWT授权Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });
            });
        }
    }
}
