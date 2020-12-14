using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Diagnostics;
using System.IO;
using Microsoft.Extensions.Hosting;

namespace CCmall.AutoService
{
    public class MainService
    {
        private string[] args;
        public MainService(string[] vs)
        {
            args = vs;
        }
        public void start()
        {
            var isService = !(Debugger.IsAttached || args.Contains("--console"));
            var builder = CreateWebHostBuilder(args.Where(arg => arg != "--console").ToArray());

            if (isService)
            {
                var pathToExe = Process.GetCurrentProcess().MainModule.FileName;
                var pathToContentRoot = Path.GetDirectoryName(pathToExe);
                builder.UseContentRoot(pathToContentRoot);
            }

            var host = builder.Build();
            host.Run();
        }
        public void stop()
        {

        }

        public IHostBuilder CreateWebHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.SetMinimumLevel(LogLevel.Trace);
                })
                .UseNLog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder
                        .ConfigureKestrel(options =>
                        {
                            options.AllowSynchronousIO = true;
                        })
                        .UseStartup<Startup>()
                        .UseUrls($"http://0.0.0.0:{(args.Length >= 1 ? int.Parse(args[1]) : 5123)}");
                });
    }
}
