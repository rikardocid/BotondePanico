using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GFDSystems.Vigitech.DAL.Entities
{
    [Table("EmergencyAlert")]
    public class EmergencyAlert
    {
        [Key]
        [Column("EmergencyAlertId")]
        public int EmergencyAlertId { get; set; }
        [Column("DateRegistration"),Required]
        public DateTime DateRegistration { get; set; }
        [Column("LatpLong"),StringLength(50),Required]
        public string Latlong { get; set; }
        [Column("PostalCode"), Required]
        public int PostalCode { get; set; }
        [Column("Address"),StringLength(70),Required]
        public string Address { get; set; }
        [Column("Reference"),StringLength(100),Required]
        public string Reference { get; set; }
        [Column("Commentary"),StringLength(150),Required]
        public string Commentary { get; set; }
        /// <summary>
        /// ///////////////////////////////
        /// </summary>
        [ForeignKey("suburb")]
        public int SuburbId { get; set; }
        public Suburb suburb { get; set; }
        [ForeignKey("citizen")]
        public int CitizenId { get; set; }
        public Citizen citizen { get; set; }
        [ForeignKey("typeEmergency")]
        public int TypeEmergencyId { get; set; }
        public TypeEmergency typeEmergency { get; set; }
        [ForeignKey("typeIncidence")]
        public int TypeIncidenceId { get; set; }
        public TypeIncidence typeIncidence { get; set; }

    }
}
