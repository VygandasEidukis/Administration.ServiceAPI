using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using EPS.Administration.DAL;
using EPS.Administration.DAL.Context;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace EPS.Administration.ServiceAPI
{
    public class Program
    {
        private static int httpPort;
        private static int httpsPort;
        private static string c_passowrd;
        private static string c_file;
        private static readonly string path = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);

        public static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile(Path.Combine(path, "appsettings.json"), optional: false, true)
                .Build();
            
            httpPort = config.GetValue<int>("phttp");
            httpsPort = config.GetValue<int>("phttps");
            c_passowrd = config.GetValue<string>("certificate_password");
            c_file = config.GetValue<string>("certificate_file");


            Log.Logger = new LoggerConfiguration()
                    .ReadFrom.Configuration(config)
                    .CreateLogger();

            var host = CreateHostBuilder(args).Build();
            //Seed the DB
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<DeviceContext>();
                DbInitializer.Seed(context);
            }

            Console.WriteLine($"PFX: {Path.Combine(path, c_file)}");

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureKestrel(x =>
                    {
                        x.ListenAnyIP(httpPort);
                        x.ListenAnyIP(httpsPort, y =>
                        {
                            y.UseHttps(Path.Combine(path, c_file), c_passowrd);
                        });
                    });
                    webBuilder.UseStartup<Startup>();
                });
    }
}
