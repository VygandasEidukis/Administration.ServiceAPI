using System.IO;
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

        public static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, true)
                .Build();

            httpPort = config.GetValue<int>("HTTP_PORT");
            httpsPort = config.GetValue<int>("HTTPS_PORT");
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

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
