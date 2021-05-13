using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GFDSystems.Vigitech.DAL.Entities
{
    [Table("Type")]
    public class Type
    {
        [Key]
        [Column("TypeId"), StringLength(5)]
        public string TypeId { get; set; }
        [Column("Name"), StringLength(30),Required]
        public string Name { get; set; }
        [ForeignKey("groupType")]
        public int GroupTypeId { get; set; }
        public GroupType groupType { get; set; }
    }
}
