using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GFDSystems.Vigitech.DAL.Entities.ControlEmergencias
{
    [Table("HistoryDeviceAssigned")]
    public class HistoryDeviceAssigned
    {
        [Key]
        [Column("HistoryDeviceAssignedId")]
        public int HistoryDeviceAssignedId { get; set; }
        [Column("CarLicence"), StringLength(10), Required]
        public int CarLicence { get; set; }
        [Column("Description"), StringLength(10), Required]
        public int Description { get; set; }//num de patrulla
        [Column("DateAsignation"), Required]
        public DateTime DateAsignation { get; set; }
        ////////////////////////////
        [ForeignKey("StatusDevice")]
        public int StatusId { get; set; }
        public StatusDevice StatusDevice { get; set; }
        [ForeignKey("SecurityAgent")]
        public int SecurityAgentID { get; set; }
        public SecurityAgent SecurityAgent { get; set; }
        [ForeignKey("DeviceAssigned")]
        public int DeviceAssignedId { get; set; }
        public DeviceAssigned DeviceAssigned { get; set; }
    }
}
