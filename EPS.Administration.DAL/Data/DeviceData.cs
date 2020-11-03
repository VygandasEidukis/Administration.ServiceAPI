using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EPS.Administration.DAL.Data
{
    public class DeviceData : IRevisionableEntity
    {
        [Key]
        [Column(Order = 0)]
        public int Id { get; set; }

        [Column(Order = 1)]
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
        public DeviceModelData Model { get; set; }

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
        public IEnumerable<DeviceEventData> DeviceEvents { get; set; }

        /// <summary>
        /// Grouping classifications of the device
        /// </summary>
        public ClassificationData Classification { get; set; }

        /// <summary>
        /// Defines current status of the device
        /// </summary>
        public DetailedStatusData Status { get; set; }

        //TODO: ADD location
    }
}
