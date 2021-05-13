using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GFDSystems.Vigitech.DAL.Entities.ControlEmergencias
{

    [Table("DeviceAssigned")]
    public class DeviceAssigned
    {
        [Key]
        [Column("DeviceAssignedId")]
        public int DeviceAssignedId { get; set; }
        [Column("CarLicence"), StringLength(10), Required]
        public int CarLicence { get; set; }
        [Column("Description"), StringLength(10), Required]
        public int Description { get; set; }//num de patrulla
        [Column("DateAsignation"), Required]
        public DateTime DateAsignation { get; set; }
        ////////////////////
        [ForeignKey("StatusDevice")]
        public int StatusId { get; set; }
        public StatusDevice StatusDevice { get; set; }
        [ForeignKey("SecurityAgent")]
        public int SecurityAgentID { get; set; }
        public SecurityAgent SecurityAgent { get; set; }
    }

}
