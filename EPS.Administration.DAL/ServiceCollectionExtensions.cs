using EPS.Administration.DAL.Context;
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

            return services;
        }
    }
}
