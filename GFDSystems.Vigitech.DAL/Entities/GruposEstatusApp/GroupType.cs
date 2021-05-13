using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GFDSystems.Vigitech.DAL.Entities
{
    [Table("GroupType")]
    public class GroupType
    {
        [Key]
        [Column("GroupTypeId")]
        public int GroupTypeID { get; set; }
        [Column("Name"), StringLength(30), Required]
        public string Name { get; set; }
        [Column("Description"), StringLength(200)]
        public string Description { get; set; }
    }
}
