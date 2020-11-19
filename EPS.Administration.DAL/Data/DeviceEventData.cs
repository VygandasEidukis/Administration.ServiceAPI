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
        public int StatusId { get; set; }
        public DateTime Date { get; set; }
        public int LocationId { get; set; }
        public int? GroupId { get; set; }
        public int DeviceDataId { get; set; }

        public DetailedStatusData Status { get; set; }
        public DeviceLocationData Location { get; set; }
    }
}
