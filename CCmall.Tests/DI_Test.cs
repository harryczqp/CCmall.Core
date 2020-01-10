using CCmall.Common.Configurations;
using Microsoft.DotNet.PlatformAbstractions;
using Microsoft.Extensions.DependencyInjection;
using System;
using Xunit;

namespace CCmall.Tests
{
    /// <summary>
    /// ·þÎñ×¢Èë
    /// </summary>
    public class DI_Test
    {
        [Fact]
        public void DI_Services_Test()
        {
            var basePath = Microsoft.DotNet.PlatformAbstractions.ApplicationEnvironment.ApplicationBasePath;
            IServiceCollection services = new ServiceCollection();
            services.AddSingleton(new Appsettings(basePath));
            Assert.True(services.Count > 0);
        }
    }
}
