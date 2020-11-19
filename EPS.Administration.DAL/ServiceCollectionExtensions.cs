using AutoMapper;
using EPS.Administration.DAL.Context;
using EPS.Administration.DAL.Data;
using EPS.Administration.DAL.Services;
using EPS.Administration.DAL.Services.ClassificationService;
using EPS.Administration.DAL.Services.DetailedStatusService;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EPS.Administration.DAL
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterDataServices(this IServiceCollection services, IConfiguration configuration)
        {
            //TODO: HIGH Move sqlConnectionString to configuration
            services.AddDbContext<DeviceContext>(
                o => o.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IDetailedStatusService, DetailedStatusService>();
            services.AddScoped<IBaseService<DetailedStatusData>, BaseService<DetailedStatusData>>();
            services.AddScoped<IBaseService<ClassificationData>, BaseService<ClassificationData>>();
            services.AddScoped<IBaseService<DeviceModelData>, BaseService<DeviceModelData>>();
            services.AddScoped<IBaseService<DeviceLocationData>, BaseService<DeviceLocationData>>();
            services.AddScoped<IBaseService<DeviceData>, BaseService<DeviceData>>();
            services.AddScoped<IBaseService<DeviceEventData>, BaseService<DeviceEventData>>();
            services.AddAutoMapper(typeof(ServiceCollectionExtensions));

            return services;
        }
    }
}
