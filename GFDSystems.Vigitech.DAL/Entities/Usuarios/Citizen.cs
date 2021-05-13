using GFDSystems.Vigitech.DAL.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GFDSystems.Vigitech.DAL.Entities
{
    [Table("Citizen")]
    public class Citizen
    {
        [Key]
        [Column("CitizenId")]
        public int CitizenId { get; set; }
        [Column("DateOfBirth")]
        public DateTime DateOfBirth { get; set; }
        [Column("CURP"), StringLength(18)]//tiene 18
        public string CURP { get; set; }
        [Column("Sex"), StringLength(20)]
        public string Sex { get; set; }
        /// //////////////////////////////////////////////////
        /// //////////////////////////////////////////////////
        /// //////////////////////////////////////////////////
        [ForeignKey("appUser")]
        public int AspNetUserId { get; set; }
        public AppUser appUser { get; set; } // string appUserId
    }
}
