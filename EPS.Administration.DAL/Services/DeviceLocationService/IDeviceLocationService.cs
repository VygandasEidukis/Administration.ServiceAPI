using EPS.Administration.Models.Device;
using System.Collections.Generic;

namespace EPS.Administration.DAL.Services.DeviceLocationService
{
    public interface IDeviceLocationService
    {
        void AddOrUpdate(DeviceLocation location);
        void AddOrUpdate(IEnumerable<DeviceLocation> locations);
        DeviceLocation Get(string name);
        DeviceLocation Get(int id);
        List<DeviceLocation> Get();
    }
}
