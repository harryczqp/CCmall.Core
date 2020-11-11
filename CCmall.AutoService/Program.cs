using System;
using Topshelf;

namespace CCmall.AutoService
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello Topshelf Demo!");
            HostFactory.Run(x =>
            {
                x.Service<MainService>(s =>                                  
                {
                    s.ConstructUsing(name => new MainService());
                });
                x.RunAsLocalSystem();

                x.SetDescription("CCmall.AutoService");
                x.SetDisplayName("CCmall.AutoService");
                x.SetServiceName("CCmall.AutoService");
            });
        }
    }
}
