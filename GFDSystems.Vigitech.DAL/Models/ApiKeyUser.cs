using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using GFDSystems.Vigitech.DAL.Models;

namespace GFDSystems.Vigitech.DAL.Models
{
    public class ApiKeyUser
    {
        [Key]
        [Column("ApiKeyUserId")]
        public int Id { get; set; }
        [Column("Key"), StringLength(50)]
        public string Key { get; set; }
        [Column("IsActive")]
        public bool IsActive { get; set; }
        public int AppUserId { get; set; }
        public AppUser User { get; set; }
    }
}
