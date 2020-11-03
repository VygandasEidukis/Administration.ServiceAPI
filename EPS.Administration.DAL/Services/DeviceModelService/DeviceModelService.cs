using EPS.Administration.DAL.Data;
using EPS.Administration.Models.Device;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EPS.Administration.DAL.Services.DeviceModelService
{
    public class DeviceModelService : IDeviceModelService
    {
        private readonly IBaseService<DeviceModelData> _deviceModelService;

        public DeviceModelService(IBaseService<DeviceModelData> baseService)
        {
            _deviceModelService = baseService;
        }

        public void AddOrUpdate(DeviceModel classification)
        {
            _deviceModelService.AddOrUpdate(ToDTO(classification));
        }

        public void AddOrUpdate(IEnumerable<DeviceModel> statuses)
        {
            var dtos = statuses.Select(x => ToDTO(x));
            foreach (var dto in dtos)
            {
                var item = _deviceModelService.GetSingle(x => x.Name == dto.Name);
                dto.Id = item == null ? 0 : item.Id;
                _deviceModelService.AddOrUpdate(dto);
            }
            _deviceModelService.Save();
        }

        private DeviceModelData ToDTO(DeviceModel deviceModel)
        {
            if (deviceModel == null)
            {
                return null;
            }

            var model = new DeviceModelData()
            {
                Id = deviceModel.Id,
                Revision = deviceModel.Revision,
                Name = deviceModel.Name,
                Description = deviceModel.Description,
            };

            return model;
        }
    }
}
