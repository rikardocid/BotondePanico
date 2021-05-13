using GFDSystems.Vigitech.DAL.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GFDSystems.Vigitech.DAL.Entities.ControlEmergencias
{
    [Table("SecurityAgent")]
    public class SecurityAgent
    {
        [Key]
        [Column("SecurityAgentId")]
        public int SecurityAgentId { get; set; }
        [Column("AgentLicence"), StringLength(50), Required]
        public string AgentLicence { get; set; }
        [Column("Status"), Required]
        public bool Status { get; set; }
        [Column("DateRegister"), Required]
        public DateTime DateRegister { get; set; }
        [Column("Rank"),StringLength(10),Required]
        public string Rank { get; set; }
        [Column("Corporation"), StringLength(10), Required]
        public string Corporation { get; set; }
        /////////////////////////////////////
        [ForeignKey("appUser")]
        public int AspNetUserId { get; set; }
        public AppUser appUser { get; set; }
    }
}