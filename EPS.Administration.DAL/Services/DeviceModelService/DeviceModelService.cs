using AutoMapper;
using EPS.Administration.DAL.Data;
using EPS.Administration.Models.Device;
using System.Collections.Generic;
using System.Linq;

namespace EPS.Administration.DAL.Services.DeviceModelService
{
    public class DeviceModelService : IDeviceModelService
    {
        private readonly IBaseService<DeviceModelData> _deviceModelService;
        private readonly IMapper _mapper;

        public DeviceModelService(IBaseService<DeviceModelData> baseService, IMapper mapper)
        {
            _deviceModelService = baseService;
            _mapper = mapper;
        }

        public void AddOrUpdate(DeviceModel model)
        {
            _deviceModelService.AddOrUpdate(_mapper.Map<DeviceModelData>(model));
        }

        public void AddOrUpdate(IEnumerable<DeviceModel> models)
        {
            var dtos = models.Select(x => _mapper.Map<DeviceModelData>(x));
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
            return _mapper.Map<DeviceModel>(modelData);
        }

        public DeviceModel GetById(int id)
        {
            var modelData = _deviceModelService.GetSingle(x => x.Id == id);
            return _mapper.Map<DeviceModel>(modelData);
        }
    }
}
