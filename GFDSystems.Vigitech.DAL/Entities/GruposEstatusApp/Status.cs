using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GFDSystems.Vigitech.DAL.Entities
{
    [Table("Status")]
    public class Status
    {
        [Key]
        [Column("StatusId"), StringLength(5)]
        public string TypeId { get; set; }
        [Column("Name"), StringLength(30), Required]
        public string Name { get; set; }
        [ForeignKey("GroupStatus")]
        public int GroupStatusId { get; set; }
        public GroupStatus GroupStatus { get; set; }

    }
}
