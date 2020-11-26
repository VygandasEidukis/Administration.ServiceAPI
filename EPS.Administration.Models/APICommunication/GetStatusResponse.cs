using EPS.Administration.Models.Device;
using System.Collections.Generic;

namespace EPS.Administration.Models.APICommunication
{
    public class GetStatusResponse : BaseResponse
    {
        public List<DetailedStatus> Statuses { get; set; }
    }
}
