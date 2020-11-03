using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EPS.Administration.DAL.Data
{
    public class DeviceLocationData : IRevisionableEntity
    {
        [Key]
        public int Id { get; set; }
        public int BaseId { get; set; }
        public int Revision { get; set; }
        public string Name { get; set; }
        public string Details { get; set; }
    }
}
