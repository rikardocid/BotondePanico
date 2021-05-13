using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GFDSystems.Vigitech.DAL.Models;
using System;


namespace GFDSystems.Vigitech.DAL.Entities
{
    [Table("State")]
    public class State
    {
        [Key]
        public int StateId { get; set; }
        [Column("Description"), StringLength(40), Required]
        public string Description { get; set; }
        [Column("Estatus")]
        public bool? Estatus { get; set; }
    }
}
