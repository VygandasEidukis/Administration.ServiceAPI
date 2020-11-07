using EPS.Administration.DAL.Data;
using EPS.Administration.Models.Device;
using System.Collections.Generic;

namespace EPS.Administration.DAL.Services.DeviceService
{
    public interface IDeviceService
    {
        void AddOrUpdate(Device device);
        void AddOrUpdate(IEnumerable<Device> devices);
        Device Get(string serialNumber);
        DeviceData ToDTO(Device device);
    }
}
