using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EPS.Administration.DAL.Data
{
    public class ClassificationData : IRevisionableEntity
    {
        /// <summary>
        /// Identification number
        /// </summary>
        [Key]
        [Column(Order = 0)]
        public int Id { get; set; }

        [Column(Order = 1)]
        public int Revision { get; set; }

        public int BaseId { get; set; }

        /// <summary>
        /// Internal code of classification, defined by business logic
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Model of classification
        /// </summary>
        public string Model { get; set; }
    }
}
