using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GFDSystems.Vigitech.DAL.Entities
{
    public class FoliosControl
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("IdUsersControl")]
        public int Id { get; set; }
        [Column("Description"), StringLength(50)]
        public string Description { get; set; }
        [Column("Prefix"), StringLength(3)]
        public string Prefix { get; set; }
        [Column("Secuencial"), Required]
        public int Secuencial { get; set; }
        [Column("Sufix"), StringLength(3)]
        public string Sufix { get; set; }
        [Column("IsActive"), Required]
        public bool IsActive { get; set; }
        [Column("Type"), StringLength(5), Required]
        public string Type { get; set; }
    }
}
