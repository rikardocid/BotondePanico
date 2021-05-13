using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GFDSystems.Vigitech.DAL.Models;
using System;


namespace GFDSystems.Vigitech.DAL.Entities
{
    [Table("Town")]
    public class Town
    {
        [Key]
        public int TownId { get; set; }
        [Column("Description"), StringLength(60), Required]
        public string Description { get; set; }
        [Column("Status")]
        public bool? Status { get; set; }
        [ForeignKey("state")]
        public int StateId { get; set; }
        public State state { get; set; }
    }
}
