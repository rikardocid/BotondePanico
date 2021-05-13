using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GFDSystems.Vigitech.DAO.Models
{
    public class PreRegisterDAO
    {
        [Required]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string RepeatPassword { get; set; }
        [Required]
        public string DeviceId { get; set; }
        [Required]
        public string MakeModel { get; set; }
        [Required]
        public string CellCompany { get; set; }
        [Required]
        public string Latitude { get; set; }
        [Required]
        public string Longitude { get; set; }
        [Required]
        public string Platform { get; set; }
        [Required]
        public string VersionOS { get; set; }
    }
}
