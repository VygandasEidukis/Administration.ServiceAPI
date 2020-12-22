using System;

namespace EPS.Administration.Models.APICommunication
{
    public class DeviceMetadataResponseWithDates
    {
        public DeviceMetadataResponse Metadatas { get; set; } = new DeviceMetadataResponse();
        public DateTime DateFrom { get; set; }
        public DateTime DateTill { get; set; }
    }
}
