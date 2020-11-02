using EPS.Administration.DAL.Context;
using EPS.Administration.DAL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace EPS.Administration.DAL.Services
{
    public class DeviceService : IDeviceService
    {
        public readonly DeviceContext _context;

        public DeviceService(DeviceContext context)
        {
            _context = context;
        }

        public List<DeviceData> Get(Expression<Func<DeviceData, bool>> func = null)
        {
            return func == null ? _context.Devices.ToList() : _context.Devices.Where(func).ToList();
        }
    }
}
