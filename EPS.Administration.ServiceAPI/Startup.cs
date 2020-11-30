using AutoMapper;
using EPS.Administration.DAL;
using EPS.Administration.DAL.Services.ClassificationService;
using EPS.Administration.DAL.Services.DetailedStatusService;
using EPS.Administration.DAL.Services.DeviceEventService;
using EPS.Administration.DAL.Services.DeviceLocationService;
using EPS.Administration.DAL.Services.DeviceModelService;
using EPS.Administration.DAL.Services.DeviceService;
using EPS.Administration.DAL.Services.FileDefinitionService;
using EPS.Administration.ServiceAPI.Helper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EPS.Administration.ServiceAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(x => x.RequireAuthenticatedSignIn = true);
            services.AddControllers();

            // configure basic authentication 
            services.AddAuthentication("BasicAuthentication")
                .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);

            // configure DI for application services
            services.AddScoped<IUserService, UserService>();

            services.AddControllersWithViews();
            services.RegisterDataServices(Configuration);

            //DI for DAL
            services.AddScoped<IDetailedStatusService, DetailedStatusService>();
            services.AddScoped<IClassificationService, ClassificationService>();
            services.AddScoped<IDeviceModelService, DeviceModelService>();
            services.AddScoped<IDeviceLocationService, DeviceLocationService>();
            services.AddScoped<IDeviceService, DeviceService>();
            services.AddScoped<IDeviceEventService, DeviceEventService>();
            services.AddScoped<IFileDefinitionService, FileDefinitionService>();

            services.AddAutoMapper(typeof(Startup));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
