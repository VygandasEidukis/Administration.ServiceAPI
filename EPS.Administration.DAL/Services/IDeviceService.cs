using EPS.Administration.DAL.Data;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EPS.Administration.DAL.Services
{
    public interface IDeviceService
    {
        List<DeviceData> Get(Expression<Func<DeviceData, bool>> func);
        Task AddOrUpdate(DeviceData device);
        Task AddOrUpdate(List<DeviceData> devices);
    }
}
