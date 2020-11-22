using AutoMapper;
using EPS.Administration.DAL.Data;
using EPS.Administration.DAL.Services.ClassificationService;
using EPS.Administration.DAL.Services.DetailedStatusService;
using EPS.Administration.DAL.Services.DeviceEventService;
using EPS.Administration.DAL.Services.DeviceLocationService;
using EPS.Administration.DAL.Services.DeviceModelService;
using EPS.Administration.Models.APICommunication;
using EPS.Administration.Models.APICommunication.Filter;
using EPS.Administration.Models.Device;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace EPS.Administration.DAL.Services.DeviceService
{
    public class DeviceService : IDeviceService
    {
        private readonly IBaseService<DeviceData> _deviceService;
        private readonly IDetailedStatusService _statusService;
        private readonly IClassificationService _classificationService;
        private readonly IDeviceLocationService _locationService;
        private readonly IDeviceModelService _modelService;
        private readonly IDeviceEventService _deviceEventService;
        private readonly IMapper _mapper;

        public DeviceService(IBaseService<DeviceData> baseService,
                             IDetailedStatusService statusService,
                             IClassificationService classificationService,
                             IDeviceLocationService locationService,
                             IDeviceModelService modelService,
                             IDeviceEventService deviceEventService,
                             IMapper mapper)
        {
            _deviceService = baseService;
            _statusService = statusService;
            _classificationService = classificationService;
            _locationService = locationService;
            _modelService = modelService;
            _deviceEventService = deviceEventService;
            _mapper = mapper;
        }

        public void AddOrUpdate(Device device)
        {
            _deviceService.AddOrUpdate(_mapper.Map<DeviceData>(device));
        }

        public void AddOrUpdate(IEnumerable<Device> devices)
        {
            int id = 0;
            try
            {
                var dtoDevices = devices.Select(device => _mapper.Map<DeviceData>(device));

                if (!dtoDevices.Any())
                {
                    return;
                }

                foreach (var dto in dtoDevices)
                {
                    var item = _deviceService.GetSingle(x => x.SerialNumber == dto.SerialNumber);
                    dto.Id = item == null ? 0 : item.Id;

                    dto.Status = null;
                    dto.OwnedBy = null;
                    dto.InitialLocation = null;
                    dto.Classification = null;
                    dto.Model = null;
                    
                    foreach (var eve in dto.DeviceEvents)
                    {
                        eve.Location = null;
                        eve.Status = null;
                    }


                    _deviceService.AddOrUpdate(dto);
                }
                _deviceService.Save();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

        }

        public Device Get(string serialNumber)
        {
            var device = _deviceService.GetSingle(x => x.SerialNumber == serialNumber);
            return _mapper.Map<Device>(device);
        }

        public List<Device> Get(DeviceFilter filter)
        {
            var devices = _deviceService.Get();

            var mappedDevices = new List<Device>();
            foreach (var device in devices)
            {
                mappedDevices.Add(_mapper.Map<Device>(device));
            }

            var qDevices = mappedDevices.AsQueryable();
            var query = filter.GetQuery();

            if (!string.IsNullOrEmpty(query) && query.Length > 5)
            {
                qDevices = qDevices.Where(query);
            }

            if (!string.IsNullOrEmpty(filter.OrderBy))
            {
                if (filter.ReverseOrder)
                {
                    qDevices = qDevices.OrderByDescending(x => filter.OrderBy);
                }
                else
                {
                    qDevices = qDevices.OrderBy(x => filter.OrderBy);
                }
            }

            return qDevices.Skip(filter.PageSize * filter.Page).Take(filter.PageSize).ToList();
        }

        public int BaseDeviceCount()
        {
            return _deviceService.Get().Where(x => x.BaseId == 0).Count();
        }

        public DeviceMetadataResponse GetMetadata()
        {
            var metadata = new DeviceMetadataResponse()
            {
                Classifications = _classificationService.Get(),
                Locations = _locationService.Get(),
                Models = _modelService.Get(),
                Statuses = _statusService.Get()
            };

            return metadata;
        }
    }
}
