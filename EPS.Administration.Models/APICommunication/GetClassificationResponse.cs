using EPS.Administration.Models.Device;
using System.Collections.Generic;

namespace EPS.Administration.Models.APICommunication
{
    public class GetClassificationResponse : BaseResponse
    {
        public List<Classification> Classifications { get; set; }
    }
}
