using AutoMapper;
using EPS.Administration.DAL.Data;
using EPS.Administration.DAL.Services.ClassificationService;
using EPS.Administration.DAL.Services.DetailedStatusService;
using EPS.Administration.DAL.Services.DeviceEventService;
using EPS.Administration.DAL.Services.DeviceLocationService;
using EPS.Administration.DAL.Services.DeviceModelService;
using EPS.Administration.DAL.Services.FileDefinitionService;
using EPS.Administration.Models.APICommunication;
using EPS.Administration.Models.APICommunication.Filter;
using EPS.Administration.Models.Device;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
        private readonly IFileDefinitionService _fileDefinitionService;
        private readonly IMapper _mapper;

        public DeviceService(IBaseService<DeviceData> baseService,
                             IDetailedStatusService statusService,
                             IClassificationService classificationService,
                             IDeviceLocationService locationService,
                             IDeviceModelService modelService,
                             IDeviceEventService deviceEventService,
                             IFileDefinitionService fileDefinitionService,
                             IMapper mapper)
        {
            _deviceService = baseService;
            _statusService = statusService;
            _classificationService = classificationService;
            _locationService = locationService;
            _modelService = modelService;
            _deviceEventService = deviceEventService;
             _fileDefinitionService = fileDefinitionService;
            _mapper = mapper;
        }

        public void AddOrUpdate(Device device)
        {
            AddOrUpdate(new[] { device });
        }

        public void AddOrUpdate(IEnumerable<Device> devices)
        {
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
                    dto.Revision = 0;
                    dto.BaseId = 0;

                    dto.Status = null;
                    dto.OwnedBy = null;
                    dto.InitialLocation = null;
                    dto.Classification = null;
                    dto.Model = null;
                    dto.Document = null;
                    
                    foreach (var eve in dto.DeviceEvents)
                    {
                        eve.Id = 0;
                        eve.BaseId = 0;
                        eve.Revision = 0;
                        eve.Location = null;
                        eve.Status = null;
                    }

                    string jsonString = JsonConvert.SerializeObject(dto);
                    JToken parsedJson = JToken.Parse(jsonString);
                    var beautified = parsedJson.ToString(Formatting.Indented);

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
            var deviceDto = _deviceService.GetSingle(x => x.SerialNumber == serialNumber);
            var device = _mapper.Map<Device>(deviceDto);

            return device;
        }

        /// <summary>
        /// Get devices by filter
        /// </summary>
        /// <param name="filter">Filters that will apply on search</param>
        /// <returns>Filtered promotions</returns>
        public List<Device> Get(DeviceFilter filter)
        {
            var devices = _deviceService.GetLatest();

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
            return _deviceService.Count();
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

        public GetLocationResponse GetLocation()
        {
            return new GetLocationResponse()
            {
                Locations = _locationService.Get()
            };
        }

        public GetClassificationResponse GetClassification()
        {
            return new GetClassificationResponse()
            {
                Classifications = _classificationService.Get()
            };
        }

        public GetStatusResponse GetStatus()
        {
            return new GetStatusResponse()
            {
                Statuses = _statusService.Get()
            };
        }

        public void UpdateModels(DeviceModel model, DeviceModel oldModel)
        {
            var devicesWithModel = _deviceService.GetLatest().Where(device => device.Model.Id == oldModel.Id);

            foreach (var device in devicesWithModel)
            {
                var dev = _mapper.Map<Device>(device);
                dev.Model = model;
                AddOrUpdate(dev);
            }
        }

        public void UpdateLocations(DeviceLocation newLocation, DeviceLocation oldLocation)
        {
            var devicesWithLocation = _deviceService.GetLatest()
                .Where(device => device.InitialLocation.Id == oldLocation.Id
                || device.OwnedBy.Id == oldLocation.Id
                || device.DeviceEvents != null
                   && device.DeviceEvents.Any(even => even.Location != null
                       && even.Location.Id == oldLocation.Id));

            foreach (var device in devicesWithLocation)
            {
                var dev = _mapper.Map<Device>(device);

                if (dev.OwnedBy.Id == oldLocation.Id)
                {
                    dev.OwnedBy = newLocation;
                }

                if (dev.InitialLocation.Id == oldLocation.Id)
                {
                    dev.InitialLocation = newLocation;
                }

                if (dev.DeviceEvents != null && dev.DeviceEvents.Any(x => x.Location.Id == oldLocation.Id))
                {
                    foreach (var even in dev.DeviceEvents)
                    {
                        if (even.Location != null && even.Location.Id == oldLocation.Id)
                        {
                            even.Location = newLocation;
                        }
                    }
                }
                AddOrUpdate(dev);
            }
        }

        public void UpdateClassifications(Classification newClassification, Classification oldClassification)
        {
            var devicesWithClassification = _deviceService.GetLatest().Where(device => device.Classification.Id == oldClassification.Id);

            foreach (var device in devicesWithClassification)
            {
                var dev = _mapper.Map<Device>(device);
                dev.Classification = newClassification;
                AddOrUpdate(dev);
            }
        }

        public void UpdateStatuses(DetailedStatus newStatus, DetailedStatus oldStatus)
        {
            var devicesWithStatus = _deviceService.GetLatest()
                            .Where(device => device.Status.Id == oldStatus.Id
                            || device.DeviceEvents != null
                               && device.DeviceEvents.Any(even => even.Status != null
                               && even.Status.Id == oldStatus.Id));

            foreach (var device in devicesWithStatus)
            {
                var dev = _mapper.Map<Device>(device);

                if (dev.Status.Id == oldStatus.Id)
                {
                    dev.Status = newStatus;
                }

                if (dev.DeviceEvents != null && dev.DeviceEvents.Any(x => x.Status.Id == oldStatus.Id))
                {
                    foreach (var even in dev.DeviceEvents)
                    {
                        if (even.Status != null && even.Status.Id == oldStatus.Id)
                        {
                            even.Status = newStatus;
                        }
                    }
                }
                AddOrUpdate(dev);
            }
        }
    }
}
