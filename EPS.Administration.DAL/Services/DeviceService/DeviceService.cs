using EPS.Administration.DAL.Data;
using EPS.Administration.DAL.Services.ClassificationService;
using EPS.Administration.DAL.Services.DetailedStatusService;
using EPS.Administration.DAL.Services.DeviceLocationService;
using EPS.Administration.DAL.Services.DeviceModelService;
using EPS.Administration.Models.Device;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public DeviceService(IBaseService<DeviceData> baseService, 
                             IDetailedStatusService statusService, 
                             IClassificationService classificationService,
                             IDeviceLocationService locationService,
                             IDeviceModelService modelService)
        {
            _deviceService = baseService;
            _statusService = statusService;
            _classificationService = classificationService;
            _locationService = locationService;
            _modelService = modelService;
        }

        public void AddOrUpdate(Device device)
        {
            _deviceService.AddOrUpdate(ToDTO(device));
        }

        public void AddOrUpdate(IEnumerable<Device> devices)
        {
            int id = 0;
            try
            {
                var dtoDevices = devices.Select(device => ToDTO(device));

                if(!dtoDevices.Any())
                {
                    return;
                }

                foreach (var dto in dtoDevices)
                {
                    var item = _deviceService.GetSingle(x => x.SerialNumber == dto.SerialNumber);
                    dto.Id = item == null ? 0 : item.Id;
                    _deviceService.AddOrUpdate(dto);
                }
                _deviceService.Save();
            }catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            
        }

        public Device Get(string serialNumber)
        {
            var device = _deviceService.GetSingle(x => x.SerialNumber == serialNumber);
            return MappingHelper<Device>.Convert(device);
        }

        public DeviceData ToDTO(Device device)
        {
            if (device == null)
            {
                return null;
            }

            var classification = _classificationService.Get(device.Classification.Code);
            var initialLocation = _locationService.GetLocation(device.InitialLocation.Name);
            var model = _modelService.Get(device.Model.Name);
            var ownedBy = _locationService.GetLocation(device.OwnedBy.Name);
            var status = _statusService.GetStatus(device.Status.Status);
            var devicetoData = new DeviceData()
            {
                AcquisitionDate = device.AcquisitionDate,
                AdditionalNotes = device.AdditionalNotes,
                BaseId = device.BaseId,
                ClassificationId = classification.Id,
                DeviceEvents = null,
                Id = device.Id,
                InitialLocationId = initialLocation.Id,
                InvoiceNumber = device.InvoiceNumber,
                ModelId = model.Id,
                Notes = device.Notes,
                OwnedById = ownedBy.Id,
                Revision = device.Revision,
                SerialNumber = device.SerialNumber,
                SfDate = device.SfDate,
                SfNumber = device.SfNumber,
                StatusId = status.Id,
            };
            return devicetoData;
        }
    }
}
