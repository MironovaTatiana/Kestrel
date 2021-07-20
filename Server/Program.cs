using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
        .ConfigureWebHostDefaults(webBuilder =>
        {
            webBuilder.ConfigureKestrel(serverOptions =>
            {
                serverOptions.Listen(IPAddress.Loopback, 5000);
                serverOptions.Listen(IPAddress.Loopback, 5001,
                    listenOptions =>
                    {
                        listenOptions.UseHttps("testCert.pfx",
                            "testPassword");
                    });
            })
            .UseStartup<Startup>();
        });
    }
}
