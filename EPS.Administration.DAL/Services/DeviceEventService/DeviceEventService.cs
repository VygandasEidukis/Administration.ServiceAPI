using AutoMapper;
using EPS.Administration.DAL.Data;
using EPS.Administration.DAL.Services.ClassificationService;
using EPS.Administration.DAL.Services.DetailedStatusService;
using EPS.Administration.DAL.Services.DeviceLocationService;
using EPS.Administration.Models.Device;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EPS.Administration.DAL.Services.DeviceEventService
{
    public class DeviceEventService : IDeviceEventService
    {
        private readonly IBaseService<DeviceEventData> _deviceEventService;
        private readonly IClassificationService _groupService;
        private readonly IDeviceLocationService _locationService;
        private readonly IDetailedStatusService _detailedStatusService;
        private readonly IMapper _mapper;

        public DeviceEventService(IBaseService<DeviceEventData> baseService,
                                  IClassificationService classificationService,
                                  IDeviceLocationService locationService,
                                  IDetailedStatusService detailedStatusService,
                                  IMapper mapper)
        {
            _deviceEventService = baseService;
            _groupService = classificationService;
            _locationService = locationService;
            _detailedStatusService = detailedStatusService;
            _mapper = mapper;
        }

        public void AddOrUpdate(DeviceEvent deviceEvent)
        {
            deviceEvent.Location = null;
            deviceEvent.Status = null;
            _deviceEventService.AddOrUpdate(_mapper.Map<DeviceEventData>(deviceEvent));
        }

        public void AddOrUpdate(IEnumerable<DeviceEvent> classifications)
        {
            var dtos = classifications.Select(x => _mapper.Map<DeviceEventData>(x));
            foreach (var dto in dtos)
            {
                var item = _deviceEventService.GetSingle(x => x.Id == dto.Id);
                dto.Id = item == null ? 0 : item.Id;
                _deviceEventService.AddOrUpdate(dto);
            }
            _deviceEventService.Save();
        }

        public DeviceEvent Get(string id)
        {
            var classification = _deviceEventService.GetSingle(x => x.Id.ToString() == id);
            return _mapper.Map<DeviceEvent>(classification);
        }
    }
}
