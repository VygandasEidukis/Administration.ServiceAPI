using EPS.Administration.DAL.Data;
using Microsoft.EntityFrameworkCore;

namespace EPS.Administration.DAL.Context
{
    public class DeviceContext : DbContext
    {
        public DbSet<DeviceData> Devices { get; set; }
        public DbSet<DeviceModelData> DeviceModels { get; set; }
        public DbSet<ClassificationData> Classifications { get; set; }
        public DbSet<DetailedStatusData> Statuses { get; set; }
        public DbSet<DeviceEventData> DeviceEvents { get; set; }

        public DeviceContext(DbContextOptions<DeviceContext> options) : base(options) { }

    }
}
