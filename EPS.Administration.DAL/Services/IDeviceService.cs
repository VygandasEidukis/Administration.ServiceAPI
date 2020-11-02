using EPS.Administration.DAL.Data;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace EPS.Administration.DAL.Services
{
    public interface IDeviceService
    {
        List<DeviceData> Get(Expression<Func<DeviceData, bool>> func);
    }
}
