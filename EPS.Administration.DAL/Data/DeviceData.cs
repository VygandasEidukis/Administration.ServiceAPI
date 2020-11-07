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
        [ForeignKey("Model_FK")]
        public int ModelId { get; set; }
        public DateTime AcquisitionDate { get; set; }
        public string InvoiceNumber { get; set; }
        public IEnumerable<DeviceEventData> DeviceEvents { get; set; }
        [ForeignKey("Classification_FK")]
        public int? ClassificationId { get; set; }
        [ForeignKey("Status_FK")]
        public int StatusId { get; set; }
        [ForeignKey("OwnedBy_FK")]
        public int OwnedById { get; set; }
        [ForeignKey("InitialLocationId_FK")]
        public int InitialLocationId { get; set; }
        public DateTime SfDate { get; set; }
        public string SfNumber { get; set; }
        public string AdditionalNotes { get; set; }
    }
}
