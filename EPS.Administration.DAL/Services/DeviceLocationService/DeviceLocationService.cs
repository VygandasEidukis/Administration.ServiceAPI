using AutoMapper;
using EPS.Administration.DAL.Data;
using EPS.Administration.Models.Device;
using System.Collections.Generic;
using System.Linq;

namespace EPS.Administration.DAL.Services.DeviceLocationService
{
    public class DeviceLocationService : IDeviceLocationService
    {
        private readonly IBaseService<DeviceLocationData> _deviceLocationService;
        private readonly IMapper _mapper;

        public DeviceLocationService(IBaseService<DeviceLocationData> baseService, IMapper mapper)
        {
            _deviceLocationService = baseService;
            _mapper = mapper;
        }

        public void AddOrUpdate(DeviceLocation classification)
        {
            _deviceLocationService.AddOrUpdate(_mapper.Map<DeviceLocationData>(classification));
        }

        public void AddOrUpdate(IEnumerable<DeviceLocation> locations)
        {
            var dtos = locations.Select(x => _mapper.Map<DeviceLocationData>(x));
            foreach (var dto in dtos)
            {
                var item = _deviceLocationService.GetSingle(x => x.Name == dto.Name);
                dto.Id = item == null ? 0 : item.Id;
                _deviceLocationService.AddOrUpdate(dto);
            }
            _deviceLocationService.Save();
        }

        public DeviceLocation Get(string name)
        {
            var location = _deviceLocationService.GetSingle(x => x.Name == name);
            return _mapper.Map<DeviceLocation>(location);
        }

        public DeviceLocation Get(int id)
        {
            var location = _deviceLocationService.GetSingle(x => x.Id == id);
            return _mapper.Map<DeviceLocation>(location);
        }

        public List<DeviceLocation> Get()
        {
            var locationsDto = _deviceLocationService.GetLatest();

            List<DeviceLocation> locations = locationsDto.Select(x => _mapper.Map<DeviceLocation>(x)).ToList();
            return locations;
        }
    }
}
