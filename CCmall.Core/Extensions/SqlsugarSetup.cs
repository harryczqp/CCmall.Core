using CCmall.Common.Configurations;
using Microsoft.Extensions.DependencyInjection;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CCmall.Core.Extensions
{
    public static class SqlsugarSetup
    {
        public static void AddSqlsugarSetup(this IServiceCollection services)
        {
            if (services ==null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.AddScoped<ISqlSugarClient>(f =>
            {
                var listConfig = new List<ConnectionConfig>();
                var configSettings= Appsettings.BaseDB;
                foreach (var setting in configSettings)
                {
                    listConfig.Add(new ConnectionConfig
                    {
                        ConfigId = setting.ConnId,
                        ConnectionString = setting.Connection,
                        DbType = (DbType)setting.DbType,
                        IsAutoCloseConnection = true,
                        IsShardSameThread = false,
                        MoreSettings = new ConnMoreSettings()
                        {
                            IsAutoRemoveDataCache = true
                        }
                    });
                }
                return new SqlSugarClient(listConfig);
            });
        }
    }
}
