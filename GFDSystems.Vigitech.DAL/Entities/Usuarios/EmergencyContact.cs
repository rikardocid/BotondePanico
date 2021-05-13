using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GFDSystems.Vigitech.DAL.Models;
using System;


namespace GFDSystems.Vigitech.DAL.Entities
{
    [Table("EmergencyContact")]
    public class EmergencyContact
    {
        [Key]
        [Column("EmergencyContactId")]
        public int EmergencyContactId { get; set; }
        [Column("Name"), StringLength(80)]
        public string Name { get; set; }
        [Column("Relationship"), StringLength(30)]
        public string Relationship { get; set; }
        [Column("PhoneNumber"), StringLength(12)]
        public string PhoneNumber { get; set; }
        [ForeignKey("citizen")]
        public int CitizenId { get; set; }
        public Citizen citizen { get; set; }
    }
}
