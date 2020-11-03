using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EPS.Administration.DAL.Data
{
    public class DeviceEventData : IRevisionableEntity
    {
        /// <summary>
        /// Identification number of an device event
        /// </summary>
        [Key]
        [Column(Order = 0)]
        public int Id { get; set; }

        [Column(Order = 1)]
        public int Revision { get; set; }

        public int BaseId { get; set; }

        /// <summary>
        /// Event occurrence date
        /// </summary>
        public DateTime Date { get; set; }
    }
}
