using System.ComponentModel.DataAnnotations;

namespace GFDSystems.Vigitech.DAO.Models
{
    public class LoginDAO
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
