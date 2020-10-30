using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Administration.Models.Device
{
    public class DetailedStatus
    {
        /// <summary>
        /// Device status
        /// </summary>
        public Status Status { get; set; }
        
        /// <summary>
        /// Device status description
        /// </summary>
        public string Description { get; set; }
    }
}
