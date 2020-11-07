﻿using EPS.Administration.DAL.Data;
using EPS.Administration.Models.Device;
using System.Collections.Generic;

namespace EPS.Administration.DAL.Services.DeviceModelService
{
    public interface IDeviceModelService
    {
        void AddOrUpdate(DeviceModel modelData);
        void AddOrUpdate(IEnumerable<DeviceModel> modelsData);
        DeviceModel Get(string modelText);
        DeviceModelData ToDTO(DeviceModel model);
    }
}