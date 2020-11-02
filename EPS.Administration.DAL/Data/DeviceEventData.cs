using System;

namespace EPS.Administration.DAL.Data
{
    public class DeviceEventData
    {
        /// <summary>
        /// Identification number of an device event
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Event occurrence date
        /// </summary>
        public DateTime Date { get; set; }
    }
}
