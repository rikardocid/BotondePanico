using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GFDSystems.Vigitech.DAL.Models;
using System;


namespace GFDSystems.Vigitech.DAL.Entities
{
    [Table("MobileDeviceRegistration")]
    public class MobileDeviceRegistration
    {
        [Key]
        public int MobileDeviceRegistrationId { get; set; }
        [Column("NumberPhone"), StringLength(15), Required]
        public string NumberPhone { get; set; }
        [Column("CellComapny"), StringLength(30),Required]
        public string CellComapny { get; set; }
        [Column("DeviceId"), StringLength(40), Required]
        public string DeviceId { get; set; }
        [Column("MakeModel"), StringLength(40)]
        public string MakeModel { get; set; }
        [Column("DateRegister"), Required]
        public DateTime DateRegister { get; set; }
        [Column("LatLangRegister"), StringLength(40), Required]
        public string LatLangRegister { get; set; }
        [Column("Platform"), StringLength(40)]
        public string Platform { get; set; }
        [Column("VersionOS"), StringLength(40)]
        public string VersionOS { get; set; }
        [ForeignKey("citizen")]
        public int CitizenId { get; set; }
        public Citizen citizen { get; set; }
    }
}
