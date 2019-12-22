﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CCmall.Common.Configurations
{
    public class Appsettings
    {
        static IConfiguration Configuration { get; set; }
        public Appsettings(string contentPath)
        {
            var Path = "appsettings.json";
            Configuration = new ConfigurationBuilder()
                .SetBasePath(contentPath)
                .Add(new JsonConfigurationSource { Path = Path, ReloadOnChange = true, Optional = false })
                .Build();
        }

        public static Jwt Jwt
        {
            get
            {
                var result = new Jwt();
                var section = Configuration.GetSection("Jwt");
                if (section != null)
                    result = section.Get<Jwt>();
                return result;
            }
        }
        public static Startup Startup
        {
            get
            {
                var result = new Startup();
                var section = Configuration.GetSection("Startup");
                if (section != null)
                    result = section.Get<Startup>();
                return result;
            }
        }
    }
}
