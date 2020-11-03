using EPS.Administration.Models.Device;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Administration.DAL.Services.DeviceLocationService
{
    public interface IDeviceLocationService
    {
        void AddOrUpdate(DeviceLocation status);
        void AddOrUpdate(IEnumerable<DeviceLocation> statuses);
    }
}
