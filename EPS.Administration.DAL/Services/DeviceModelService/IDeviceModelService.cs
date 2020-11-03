using EPS.Administration.Models.Device;
using System.Collections.Generic;

namespace EPS.Administration.DAL.Services.DeviceModelService
{
    public interface IDeviceModelService
    {
        void AddOrUpdate(DeviceModel status);
        void AddOrUpdate(IEnumerable<DeviceModel> statuses);
    }
}
