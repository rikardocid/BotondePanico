using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GFDSystems.Vigitech.DAL.Models;
using System;


namespace GFDSystems.Vigitech.DAL.Entities
{
    [Table("Address")]
    public class Address
    {
        [Key]
        [Column("AddressId")]
        public int AddressId { get; set; }
        [Column("Street"), StringLength(70), Required]
        public string Street { get; set; }
        [Column("ExternalNumber"),StringLength(10), Required]
        public string ExternalNumber { get; set; }
        [Column("InternalNumber"), StringLength(10)]
        public string InternalNumber { get; set; }
        //[Column("PostalCode"), Required]
        //public int PostalCode { get; set; }
        [ForeignKey("suburb")]
        public int SuburbId { get; set; }
        public Suburb suburb { get; set; }

        [ForeignKey("citizen")]
        public int CitizenId { get; set; }
        public Citizen citizen { get; set; }
    }
}
