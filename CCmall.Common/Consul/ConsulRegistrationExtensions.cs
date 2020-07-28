using Consul;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace CCmall.Common.Consul
{
    // consul服务注册扩展类
    public static class ConsulRegistrationExtensions
    {
        public static void AddConsul(this IServiceCollection service)
        {
            // 读取服务配置文件
            //var config = new ConfigurationBuilder().AddJsonFile("consulconfig.json").Build();
            //service.Configure<ConsulServiceOptions>(config);
        }

        public static IApplicationBuilder UseConsul(this IApplicationBuilder app)
        {
            // 获取主机生命周期管理接口
            var lifetime = app.ApplicationServices.GetRequiredService<IHostApplicationLifetime>();

            // 获取服务配置项
            var serviceOptions = app.ApplicationServices.GetRequiredService<IOptions<ConsulServiceOptions>>().Value;

            // 服务ID必须保证唯一
            serviceOptions.ServiceId = Guid.NewGuid().ToString();

            var consulClient = new ConsulClient(configuration =>
            {
                //服务注册的地址，集群中任意一个地址
                configuration.Address = new Uri(serviceOptions.ConsulAddress);
            });

            // 获取当前服务地址和端口，配置方式
            var uri = new Uri(serviceOptions.ServiceAddress);

            // 节点服务注册对象
            var registration = new AgentServiceRegistration()
            {
                ID = serviceOptions.ServiceId,
                Name = serviceOptions.ServiceName,// 服务名
                Address = uri.Host,
                Port = uri.Port, // 服务端口
                Check = new AgentServiceCheck
                {
                    // 注册超时
                    Timeout = TimeSpan.FromSeconds(5),
                    // 服务停止多久后注销服务
                    DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(5),
                    // 健康检查地址
                    HTTP = $"{uri.Scheme}://{uri.Host}:{uri.Port}{serviceOptions.HealthCheck}",
                    // 健康检查时间间隔
                    Interval = TimeSpan.FromSeconds(10),
                }
            };

            // 注册服务
            consulClient.Agent.ServiceRegister(registration).Wait();

            // 应用程序终止时，注销服务
            lifetime.ApplicationStopping.Register(() =>
            {
                consulClient.Agent.ServiceDeregister(serviceOptions.ServiceId).Wait();
            });

            return app;
        }
    }

    // Consul配置模型类
    public class ConsulServiceOptions
    {
        // 服务注册地址（Consul的地址）
        public string ConsulAddress { get; set; }

        // 服务ID
        public string ServiceId { get; set; }

        // 服务名称
        public string ServiceName { get; set; }

        // 健康检查地址
        public string HealthCheck { get; set; }

        //站点地址
        public string ServiceAddress { get; set; }
    }
}
