using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EPS.Administration.DAL.Data
{
    public class DeviceEventData : IRevisionableEntity
    {
        public int Id { get; set; }
        public int Revision { get; set; }
        public int BaseId { get; set; }
        [ForeignKey("status_FK")]
        public int StatusId { get; set; }
        public DateTime Date { get; set; }
        [ForeignKey("location_FK")]
        public int LocationId { get; set; }
        [ForeignKey("group_FK")]
        public int? GroupId { get; set; }
        public int DeviceDataId { get; set; }
    }
}
