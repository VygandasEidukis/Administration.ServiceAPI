using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Administration.Models.APICommunication
{
    public class BaseResponse
    {
        public ErrorCode Error { get; set; }
        public string Message { get; set; }
    }
}
