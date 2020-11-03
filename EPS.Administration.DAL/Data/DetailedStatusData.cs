using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EPS.Administration.DAL.Data
{
    public class DetailedStatusData : IRevisionableEntity
    {
        [Key]
        [Column(Order = 0)]
        public int Id { get; set; }

        [Key]
        [Column(Order = 1)]
        public int Revision { get; set; }

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
