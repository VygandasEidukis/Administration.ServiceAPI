using EPS.Administration.DAL.Data;
using EPS.Administration.Models.Device;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Administration.DAL.Services.DeviceEventService
{
    public interface IDeviceEventService
    {
        void AddOrUpdate(DeviceEvent classification);
        void AddOrUpdate(IEnumerable<DeviceEvent> classifications);
        DeviceEvent Get(string code);
        DeviceEventData ToDTO(DeviceEvent classification);
    }
}
