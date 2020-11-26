using EPS.Administration.Models.Device;
using System.Collections.Generic;

namespace EPS.Administration.Models.APICommunication
{
    public class GetLocationResponse : BaseResponse
    {
        public List<DeviceLocation> Locations { get; set; }
    }
}
