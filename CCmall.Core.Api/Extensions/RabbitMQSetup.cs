﻿using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CCmall.Core.Api.Extensions
{
    public static class RabbitMQSetup
    {
        public static void AddRabbitMQSetup(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            //if (Appsettings.app(new string[] { "RabbitMQ", "Enabled" }).ObjToBool())
            //{
            //    services.AddSingleton<IRabbitMQPersistentConnection>(sp =>
            //    {
            //        var logger = sp.GetRequiredService<ILogger<RabbitMQPersistentConnection>>();

            //        var factory = new ConnectionFactory()
            //        {
            //            HostName = Appsettings.app(new string[] { "RabbitMQ", "Connection" }),
            //            DispatchConsumersAsync = true
            //        };

            //        if (!string.IsNullOrEmpty(Appsettings.app(new string[] { "RabbitMQ", "UserName" })))
            //        {
            //            factory.UserName = Appsettings.app(new string[] { "RabbitMQ", "UserName" });
            //        }

            //        if (!string.IsNullOrEmpty(Appsettings.app(new string[] { "RabbitMQ", "Password" })))
            //        {
            //            factory.Password = Appsettings.app(new string[] { "RabbitMQ", "Password" });
            //        }

            //        var retryCount = 5;
            //        if (!string.IsNullOrEmpty(Appsettings.app(new string[] { "RabbitMQ", "RetryCount" })))
            //        {
            //            retryCount = int.Parse(Appsettings.app(new string[] { "RabbitMQ", "RetryCount" }));
            //        }

            //        return new RabbitMQPersistentConnection(factory, logger, retryCount);
            //    });
            //}
        }
    }
}
