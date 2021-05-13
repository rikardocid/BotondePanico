using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GFDSystems.Vigitech.DAL.Entities.Usuarios
{
    [Table("MobileDeviceRegistrationTemp")]
    public class MobileDeviceRegistrationTemp
    {
        [Key]
        public int MobileDeviceRegistrationId { get; set; }
        [Column("NumberPhone"), StringLength(15), Required]
        public string NumberPhone { get; set; }
        [Column("CellComapny"), StringLength(30), Required]
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
        public bool IsCompleteRegister { get; set; }
        [Column("AppUserId")]
        public int AppUserId { get; set; }
    }
}
