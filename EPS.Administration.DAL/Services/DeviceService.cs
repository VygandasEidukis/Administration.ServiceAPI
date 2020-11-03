using EPS.Administration.DAL.Context;
using EPS.Administration.DAL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace EPS.Administration.DAL.Services
{
    public class DeviceService : IDeviceService
    {
        public readonly DeviceContext _context;

        public DeviceService(DeviceContext context)
        {
            _context = context;
        }

        public async Task AddOrUpdate(DeviceData device)
        {
            try
            {
                await _context.Devices.AddAsync(device);
            }
            catch (Exception ex)
            {
                //TODO HIGH: Add Logs
            }
        }

        public async Task AddOrUpdate(List<DeviceData> devices)
        {
            try
            {
                await _context.Devices.AddRangeAsync(devices);
            }
            catch (Exception ex)
            {
                //TODO HIGH: Add Logs
            }
        }

        public List<DeviceData> Get(Expression<Func<DeviceData, bool>> func = null)
        {
            return func == null ? _context.Devices.ToList() : _context.Devices.Where(func).ToList();
        }
    }
}
