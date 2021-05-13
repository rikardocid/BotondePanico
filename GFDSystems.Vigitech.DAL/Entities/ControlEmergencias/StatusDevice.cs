using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GFDSystems.Vigitech.DAL.Entities.ControlEmergencias
{
    [Table("StatusDevice")]
    public class StatusDevice
    {
        [Key]
        [Column("StatusDeviceID")]
        public int StatusDeviceID { get; set; }
        [Column("Description"), StringLength(20), Required]
        public int Description { get; set; }
    }
}

/*
1 libre
2 en emergencia 
3 fuera de servicio 
 */