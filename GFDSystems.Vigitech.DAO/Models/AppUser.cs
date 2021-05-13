using GFDSystems.Vigitech.DAL.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GFDSystems.Vigitech.DAO.Models
{
    public class AppUser : IdentityUser<int>
    {
        [Column("name"), StringLength(50), Required]
        public string Name { get; set; }
        [Column("middleName"), StringLength(50), Required]
        public string MiddleName { get; set; }
        [Column("lastName"), StringLength(50), Required]
        public string LastName { get; set; }
        [Column("profilePicture")]
        public byte[] ProfilePicture { get; set; }
        [Column("firebaseId"), StringLength(255)]
        public string FirebaseId { get; set; }
        [Column("firebasePassword"), StringLength(50)]
        public string FirebasePassword { get; set; }
        [Column("authValidationCode"), StringLength(15)]
        public string AuthValidationCode { get; set; }
        [Column("isActive")]
        public bool IsActive { get; set; }
        [Required]
        [Column("type"), StringLength(5)]
        public string Type { get; set; }

        public ApiKeyUser ApiKeyUser { get; set; }
    }
}
