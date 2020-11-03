using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Administration.Models.Device
{
    public class DeviceEvent : IRevisionable
    {
        public int Id { get; set; }
        public int Revision { get; set; }

        /// <summary>
        /// Event occurrence date
        /// </summary>
        public DateTime Date { get; set; }
    }
}
