using System.Collections.Generic;

namespace EPS.Administration.Models.APICommunication
{
    public class GetDevicesResponse : BaseResponse
    {
        public List<Device.Device> Devices { get; set; }
        public int Count { get; set; }
        public int From { get; set; }
        public int Pages { get; set; }
        public int Overall { get; set; }
    }
}
