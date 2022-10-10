using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Hosting;
using System;
using System.Net;

namespace SuperPastel.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(builder =>
                {
                    builder.UseStartup<Startup>();

                    // For running in Railway
                    var portVar = Environment.GetEnvironmentVariable("PORT");
                    if (portVar is { Length: > 0 } && int.TryParse(portVar, out int port))
                    {
                        builder.ConfigureKestrel((context, options) =>
                        {
                            options.Listen(IPAddress.Any, port, listenOptions =>
                            {
                                listenOptions.Protocols = HttpProtocols.Http1AndHttp2;
                                listenOptions.UseHttps();
                            });
                        });
                    }
                });
    }
}
