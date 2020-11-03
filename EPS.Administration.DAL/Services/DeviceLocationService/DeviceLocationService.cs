using EPS.Administration.DAL.Data;
using EPS.Administration.Models.Device;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EPS.Administration.DAL.Services.DeviceLocationService
{
    public class DeviceLocationService : IDeviceLocationService
    {
        private readonly IBaseService<DeviceLocationData> _deviceLocationService;

        public DeviceLocationService(IBaseService<DeviceLocationData> baseService)
        {
            _deviceLocationService = baseService;
        }

        public void AddOrUpdate(DeviceLocation classification)
        {
            _deviceLocationService.AddOrUpdate(ToDTO(classification));
        }

        public void AddOrUpdate(IEnumerable<DeviceLocation> locations)
        {
            var dtos = locations.Select(x => ToDTO(x));
            foreach (var dto in dtos)
            {
                var item = _deviceLocationService.GetSingle(x => x.Name == dto.Name);
                dto.Id = item == null ? 0 : item.Id;
                _deviceLocationService.AddOrUpdate(dto);
            }
            _deviceLocationService.Save();
        }

        private DeviceLocationData ToDTO(DeviceLocation deviceModel)
        {
            if (deviceModel == null)
            {
                return null;
            }

            var model = new DeviceLocationData()
            {
                Id = deviceModel.Id,
                Revision = deviceModel.Revision,
                Name = deviceModel.Name,
                Details = deviceModel.Details,
            };

            return model;
        }
    }
}
