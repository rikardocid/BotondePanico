using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GFDSystems.Vigitech.DAL.Entities
{
    [Table("GroupStatus")]
    public class GroupStatus
    {
        [Key]
        [Column("GroupStatusId")]
        public int GroupStatusId { get; set; }
        [Column("Name"), StringLength(30),Required]
        public string Name { get; set; }
        [Column("Description"), StringLength(200)]
        public string Description { get; set; }
    }
}
