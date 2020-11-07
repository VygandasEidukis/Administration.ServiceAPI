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

        public DeviceEventService(IBaseService<DeviceEventData> baseService,
                                  IClassificationService classificationService,
                                  IDeviceLocationService locationService,
                                  IDetailedStatusService detailedStatusService)
        {
            _deviceEventService = baseService;
            _groupService = classificationService;
            _locationService = locationService;
            _detailedStatusService = detailedStatusService;
        }

        public void AddOrUpdate(DeviceEvent deviceEvent)
        {
            _deviceEventService.AddOrUpdate(ToDTO(deviceEvent));
        }

        public void AddOrUpdate(IEnumerable<DeviceEvent> classifications)
        {
            var dtos = classifications.Select(x => ToDTO(x));
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
            return ToDTO(classification);
        }

        public DeviceEventData ToDTO(DeviceEvent classification)
        {
            return new DeviceEventData
            {
                Id = classification.Id,
                BaseId = 0,
                Date = classification.Date,
                GroupId = classification.Group?.Id,
                LocationId = classification.Location.Id,
                Revision = classification.Revision,
                StatusId = classification.Status.Id
            };
        }
        private DeviceEvent ToDTO(DeviceEventData classification)
        {
            return new DeviceEvent()
            {
                Id = classification.Id,
                Date = classification.Date,
                Group = classification.GroupId != null ? _groupService.Get(classification.GroupId.Value) : null,
                Location = _locationService.GetLocation(classification.Id),
                Revision = classification.Revision,
                Status = _detailedStatusService.GetStatus(classification.Id)
            };
        }
    }
}
