using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Administration.Models.Device
{
    public class DeviceEvent : IRevisionable
    {
        public int Id { get; set; }
        public int Revision { get; set; }
        public DetailedStatus Status { get; set; }
        public DateTime Date { get; set; }
        public DeviceLocation Location { get; set; }
        public Classification Group { get; set; }
    }
}
