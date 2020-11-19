using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EPS.Administration.DAL.Data
{
    public class DeviceData : IRevisionableEntity
    {
        [Key]
        public int Id { get; set; }
        public int Revision { get; set; }
        public int BaseId { get; set; }
        public string SerialNumber { get; set; }
        public string Notes { get; set; }
        public int ModelId { get; set; }
        public DateTime AcquisitionDate { get; set; }
        public string InvoiceNumber { get; set; }
        public IEnumerable<DeviceEventData> DeviceEvents { get; set; }
        public int? ClassificationId { get; set; }
        public int StatusId { get; set; }
        public int OwnedById { get; set; }
        public int InitialLocationId { get; set; }
        public DateTime SfDate { get; set; }
        public string SfNumber { get; set; }
        public string AdditionalNotes { get; set; }

        public DetailedStatusData Status { get; set; }
        public DeviceModelData Model { get; set; }
        public ClassificationData Classification { get; set; }
        public DeviceLocationData InitialLocation { get; set; }
        public DeviceLocationData OwnedBy { get; set; }
    }
}
