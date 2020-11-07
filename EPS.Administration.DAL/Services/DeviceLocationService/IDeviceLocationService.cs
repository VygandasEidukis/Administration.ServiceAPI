using EPS.Administration.DAL.Data;
using EPS.Administration.Models.Device;
using System.Collections.Generic;

namespace EPS.Administration.DAL.Services.DeviceLocationService
{
    public interface IDeviceLocationService
    {
        void AddOrUpdate(DeviceLocation location);
        void AddOrUpdate(IEnumerable<DeviceLocation> locations);
        DeviceLocation GetLocation(string name);
        DeviceLocationData ToDTO(DeviceLocation deviceLocation);
    }
}
