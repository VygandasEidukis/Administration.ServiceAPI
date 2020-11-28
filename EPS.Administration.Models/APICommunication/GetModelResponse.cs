using EPS.Administration.Models.Device;
using System.Collections.Generic;

namespace EPS.Administration.Models.APICommunication
{
    public class GetModelResponse : BaseResponse
    {
        public List<DeviceModel> Models { get; set; }
    }
}
