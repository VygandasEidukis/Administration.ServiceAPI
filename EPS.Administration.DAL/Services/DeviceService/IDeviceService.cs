using EPS.Administration.Models.APICommunication;
using EPS.Administration.Models.APICommunication.Filter;
using EPS.Administration.Models.Device;
using System.Collections.Generic;

namespace EPS.Administration.DAL.Services.DeviceService
{
    public interface IDeviceService
    {
        void AddOrUpdate(Device device);
        void AddOrUpdate(IEnumerable<Device> devices);
        Device Get(string serialNumber);
        List<Device> Get(DeviceFilter filter);
        int BaseDeviceCount();
        DeviceMetadataResponse GetMetadata();
        GetLocationResponse GetLocation();
        GetClassificationResponse GetClassification();
        GetStatusResponse GetStatus();
        void UpdateModels(DeviceModel model, DeviceModel oldModel);
        void UpdateLocations(DeviceLocation newLocation, DeviceLocation oldLocation);
        void UpdateClassifications(Classification newClassification, Classification oldClassification);
        void UpdateStatuses(DetailedStatus newStatus, DetailedStatus oldStatus);
    }
}
