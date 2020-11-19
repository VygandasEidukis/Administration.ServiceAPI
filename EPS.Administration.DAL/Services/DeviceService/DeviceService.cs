using AutoMapper;
using EPS.Administration.DAL.Data;
using EPS.Administration.DAL.Services.ClassificationService;
using EPS.Administration.DAL.Services.DetailedStatusService;
using EPS.Administration.DAL.Services.DeviceEventService;
using EPS.Administration.DAL.Services.DeviceLocationService;
using EPS.Administration.DAL.Services.DeviceModelService;
using EPS.Administration.Models.Device;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;

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

        public List<Device> Get(int from, int count, string query, string orderbyQuery, bool reversed)
        {
            var devices = _deviceService.Get();

            var mappedDevices = new List<Device>();
            foreach (var device in devices)
            {
                mappedDevices.Add(_mapper.Map<Device>(device));
            }

            var qDevices = mappedDevices.AsQueryable().Where(query);
            if (!string.IsNullOrEmpty(orderbyQuery))
            {
                if (reversed)
                {
                    qDevices = qDevices.OrderByDescending(x => orderbyQuery);
                }
                else
                {
                    qDevices = qDevices.OrderBy(x => orderbyQuery);
                }
            }

            return qDevices.Skip(from).Take(count).ToList();
        }

        public int BaseDeviceCount()
        {
            return _deviceService.Get().Where(x => x.BaseId == 0).Count();
        }
    }
}
