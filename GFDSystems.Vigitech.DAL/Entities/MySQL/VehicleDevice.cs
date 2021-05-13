using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GFDSystems.Vigitech.DAL.Entities.MySQL
{
    [Table("VehicleDevice")]
    public class VehicleDevice
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }
        [Column("CarLicence"), StringLength(50),Required]
        public string CarLicence { get; set; }
        [Column("EconomicNumber"), StringLength(15)]
        public string EconomicNumber { get; set; }
        [Column("DeviceID"),StringLength(50)]
        public string DeviceID { get; set; }
        [Column("RegisterIP"),StringLength(50)]
        public string RegisterIP { get; set; }
        [Column("RegisterPort")]
        public int RegisterPort { get; set; }
        [Column("TransmitIP"),StringLength(50)]
        public string TransmitIP { get; set; }
        [Column("TransmitPort")]
        public int TransmitPort { get; set; }
        [Column("GroupID")]
        public int GroupID { get; set; }
    }
}
