using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GFDSystems.Vigitech.DAO.Models
{
    public class RegisterUserPlatformDAO
    {
        public int Id{ get; set; }
        public string UserName { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string SecondLastName { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        public string Employment { get; set; }
        [Required]
        public string RoleName { get; set; }
        public bool IsActive { get; set; }
    }
}
