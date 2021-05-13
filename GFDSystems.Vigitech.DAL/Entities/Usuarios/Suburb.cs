using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GFDSystems.Vigitech.DAL.Models;
using System;


namespace GFDSystems.Vigitech.DAL.Entities
{
    [Table("Suburb")]
    public class Suburb
    {
        [Key]
        [Column("SuburbId")]
        public int SuburbId { get; set; }
        [Column("Description"),StringLength(60),Required]
        public string Description { get; set; }
        [Column("PostalCode")]
        public int PostalCode { get; set; }
        [Column("Status")]
        public bool? Status { get; set; }
        [ForeignKey("town")]
        public int TownId { get; set; }
        public Town town { get; set; }
    }
}
