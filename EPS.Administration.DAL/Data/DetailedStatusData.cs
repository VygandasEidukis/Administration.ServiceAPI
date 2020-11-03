using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EPS.Administration.DAL.Data
{
    public class DetailedStatusData : IRevisionableEntity
    {
        [Key]
        [Column(Order = 0)]
        public int Id { get; set; }

        [Column(Order = 1)]
        public int Revision { get; set; }

        public int BaseId { get; set; }

        /// <summary>
        /// Device status
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Device status description
        /// </summary>
        public string Description { get; set; }
    }
}
