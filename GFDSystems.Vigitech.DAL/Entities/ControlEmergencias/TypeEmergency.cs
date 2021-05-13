using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GFDSystems.Vigitech.DAL.Entities
{
    [Table("TypeEmergency")]
    public class TypeEmergency
    {
        [Key]
        [Column("TypeEmergencyId")]
        public int TypeEmergencyId { get; set; }
        [Column("Name"), StringLength(30), Required]
        public string Name { get; set; }
        [Column("Status")]
        public bool? Status { get; set; }
        [Column("Acronym"),StringLength(5),Required]
        public string Acronym { get; set; }
    }
}
