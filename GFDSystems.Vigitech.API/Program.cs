using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GFDSystems.Vigitech.API
{
#pragma warning disable CS1591
    public class Program
    {
        public static void Main(string[] args)
        {
#if DEBUG
            CreateHostBuilder(args).Build().Run();
#else
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false)
                .Build(); /// el config solo es para kestrel

            var host = new WebHostBuilder().UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseConfiguration(config)
                .UseUrls("http://localhost:5001")
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();

            host.Run();
#endif
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
#pragma warning restore CS1591
}
