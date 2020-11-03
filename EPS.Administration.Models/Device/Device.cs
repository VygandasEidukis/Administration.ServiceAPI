using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Administration.Models.Device
{
    public class Device : IRevisionable
    {
        public int Id { get; set; }
        public int Revision { get; set; }

        // TODO: What if device does not have serial number?
        /// <summary>
        /// Unique serial number of the device
        /// </summary>
        public string SerialNumber { get; set; }

        /// <summary>
        /// Notes about the device
        /// </summary>
        public string Notes { get; set; }

        /// <summary>
        /// Model of a device
        /// </summary>
        public DeviceModel Model { get; set; }

        /// <summary>
        /// Date of purchase of a the device
        /// </summary>
        public DateTime AcquisitionDate { get; set; }

        /// <summary>
        /// Identification number of the invoice
        /// </summary>
        public string InvoiceNumber { get; set; }

        /// <summary>
        /// Event revisions of the device
        /// </summary>
        public IEnumerable<DeviceEvent> DeviceEvents { get; set; }

        /// <summary>
        /// Grouping classifications of the device
        /// </summary>
        public Classification Classification { get; set; }

        /// <summary>
        /// Defines current status of the device
        /// </summary>
        public DetailedStatus Status { get; set; }

        //TODO: ADD location
    }
}
