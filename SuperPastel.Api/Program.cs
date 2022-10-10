using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;

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
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();

                    // For running in Railway
                    //var portVar = Environment.GetEnvironmentVariable("PORT");
                    //if (portVar is { Length: > 0 } && int.TryParse(portVar, out int port))
                    //{
                    //    webBuilder.ConfigureKestrel(options =>
                    //    {
                    //        options.ListenAnyIP(port);
                    //    });
                    //}
                });
    }
}
