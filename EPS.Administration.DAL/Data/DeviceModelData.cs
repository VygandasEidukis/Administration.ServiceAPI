using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EPS.Administration.DAL.Data
{
    public class DeviceModelData : IRevisionableEntity
    {
        [Key]
        [Column(Order = 0)]
        public int Id { get; set; }
        [Key]
        [Column(Order = 1)]
        public int Revision { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
