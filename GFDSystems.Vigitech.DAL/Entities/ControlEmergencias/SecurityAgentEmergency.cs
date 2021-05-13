using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GFDSystems.Vigitech.DAL.Entities.ControlEmergencias
{
    [Table("SecurityAgentEmergency")]
    public class SecurityAgentEmergency
    {
        [Key] 
        [Column("SecurityAgentEmergencyId")]
        public int SecurityAgentEmergencyId { get; set; }
        [Column("Folio"), StringLength(12), Required]
        public string Folio { get; set; }
        [Column("Status"), StringLength(5), Required]
        public string Status { get; set; }
        [Column("PatrolNumber"), StringLength(5), Required]
        public string PatrolNumber { get; set; }
        /// /////////////////////////
        [ForeignKey("securityAgent")]
        public int SecurityAgentId { get; set; }
        public SecurityAgent securityAgent { get; set; }
        [ForeignKey("emergencyAlert")]
        public int EmergencyAlertId { get; set; }
        public EmergencyAlert emergencyAlert { get; set; }

    }
}
