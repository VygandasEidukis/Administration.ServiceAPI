using EPS.Administration.DAL.Data;
using EPS.Administration.Models.Device;
using System.Collections.Generic;
using System.Linq;

namespace EPS.Administration.DAL.Services.DeviceModelService
{
    public class DeviceModelService : IDeviceModelService
    {
        private readonly IBaseService<DeviceModelData> _deviceModelService;

        public DeviceModelService(IBaseService<DeviceModelData> baseService)
        {
            _deviceModelService = baseService;
        }

        public void AddOrUpdate(DeviceModel model)
        {
            _deviceModelService.AddOrUpdate(ToDTO(model));
        }

        public void AddOrUpdate(IEnumerable<DeviceModel> models)
        {
            var dtos = models.Select(x => ToDTO(x));
            foreach (var dto in dtos)
            {
                var item = _deviceModelService.GetSingle(x => x.Name == dto.Name);
                dto.Id = item == null ? 0 : item.Id;
                _deviceModelService.AddOrUpdate(dto);
            }
            _deviceModelService.Save();
        }

        public DeviceModel Get(string model)
        {
            var modelData = _deviceModelService.GetSingle(x => x.Name == model);
            return MappingHelper<DeviceModel>.Convert(modelData);
        }

        public DeviceModel GetById(int id)
        {
            var modelData = _deviceModelService.GetSingle(x => x.Id == id);
            return MappingHelper<DeviceModel>.Convert(modelData);
        }

        public  DeviceModelData ToDTO(DeviceModel deviceModel)
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
