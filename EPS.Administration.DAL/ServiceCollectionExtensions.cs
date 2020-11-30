using AutoMapper;
using EPS.Administration.DAL.Context;
using EPS.Administration.DAL.Data;
using EPS.Administration.DAL.Services;
using EPS.Administration.DAL.Services.DetailedStatusService;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace EPS.Administration.DAL
{
    public static class ServiceCollectionExtensions
    {
        public static string DocumentPath { get; private set; }
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
            services.AddScoped<IBaseService<FileDefinitionData>, BaseService<FileDefinitionData>>();
            services.AddAutoMapper(typeof(ServiceCollectionExtensions));

            DocumentPath = configuration["DocumentStorage"];

            if (string.IsNullOrEmpty(DocumentPath))
            {
                throw new NullReferenceException("DocumentStorage is not set or not valid");
            }


            return services;
        }
    }
}
