using System;
using System.Collections.Generic;

namespace EPS.Administration.Models.Device
{
    public class Device : IRevisionable
    {
        public int Id { get; set; }
        public int Revision { get; set; }
        public int BaseId { get; set; }

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
        public List<DeviceEvent> DeviceEvents { get; set; }

        public string LastUpdate
        {
            get
            {
                return DeviceEvents == null || DeviceEvents.Count == 0 ? 
                    AcquisitionDate.ToString("yyyy-MM-dd") : 
                    DeviceEvents[DeviceEventsCount-1].Date.ToString("yyyy-MM-dd");
            }
        }

        /// <summary>
        /// Gets number of device events
        /// </summary>
        public int DeviceEventsCount { 
            get 
            {
                return DeviceEvents == null ? 0 : DeviceEvents.Count; 
            } 
        }

        /// <summary>
        /// Grouping classifications of the device
        /// </summary>
        public Classification Classification { get; set; }

        /// <summary>
        /// Defines current status of the device
        /// </summary>
        public DetailedStatus Status { get; set; }

        /// <summary>
        /// Defines location of institution that device is owned by
        /// </summary>
        public DeviceLocation OwnedBy { get; set; }

        /// <summary>
        /// Defines location of institution where device was on purchase
        /// </summary>
        public DeviceLocation InitialLocation { get; set; }

        /// <summary>
        /// SF date
        /// </summary>
        public DateTime SfDate { get; set; }

        /// <summary>
        /// Sf number
        /// </summary>
        public string SfNumber { get; set; }

        /// <summary>
        /// Additional notes
        /// </summary>
        public string AdditionalNotes { get; set; }

        public Device()
        {
            if (SfDate == DateTime.MinValue)
            {
                SfDate = DateTime.Now;
            }

            if (AcquisitionDate == DateTime.MinValue)
            {
                AcquisitionDate = DateTime.Now;
            }
        }
    }
}