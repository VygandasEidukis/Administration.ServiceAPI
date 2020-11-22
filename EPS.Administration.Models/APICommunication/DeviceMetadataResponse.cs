using EPS.Administration.Models.Device;
using System.Collections.Generic;

namespace EPS.Administration.Models.APICommunication
{
    public class DeviceMetadataResponse : BaseResponse
    {
        public List<Classification> Classifications { get; set; }
        public List<DeviceLocation> Locations { get; set; }
        public List<DeviceModel> Models { get; set; }
        public List<DetailedStatus> Statuses { get; set; }
    }
}
