using GFDSystems.Vigitech.DAL.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GFDSystems.Vigitech.DAL.Entities
{
    [Table("EmergencyDirectory")]
    public class EmergencyDirectory
    {
        [Key]
        [Column("EmergencyDirectoryId")]
        public int EmergencyDirectoryId { get; set; }
        [Column("NameArea"), StringLength(40), Required]
        public string NameArea { get; set; }
        [Column("Phone"), StringLength(12), Required]
        public string Phone { get; set; }
        [Column("LatLang"), StringLength(50), Required]
        public string LatLong { get; set; }
    }
}
