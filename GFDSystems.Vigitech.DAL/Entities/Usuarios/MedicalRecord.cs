using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GFDSystems.Vigitech.DAL.Models;
using System;

namespace GFDSystems.Vigitech.DAL.Entities
{
    [Table("MedicalRecord")]
    public class MedicalRecord
    {
        [Key]
        public int MedicalRecordId { get; set; }
        [Column("Suffering"), StringLength(100)]
        public string Suffering { get; set; }
        [Column("Alergies"), StringLength(100)]
        public string Alergies { get; set; }
        [Column("Medicines"), StringLength(100)]
        public string Medicines { get; set; }
        [Column("BloodType"), StringLength(10)]
        public string BloodType { get; set; }
        [ForeignKey("citizen")]
        public int CitizenId { get; set; }
        public Citizen citizen { get; set; }
    }
}
