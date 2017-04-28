using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Hosting;

namespace EntityFramework_6534
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine($"`int` is in {typeof(int).GetTypeInfo().Assembly.Location}.");

            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .UseApplicationInsights()
                .Build();

            host.Run();
        }
    }
}
