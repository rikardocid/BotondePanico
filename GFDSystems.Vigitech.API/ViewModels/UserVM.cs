using System.ComponentModel.DataAnnotations;

namespace GFDSystems.Vigitech.API.ViewModels
{
    public class UserVM
    {
        [StringLength(50), Required]
        public string UserName { get; set; }
        [StringLength(50), Required]
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        [StringLength(50), Required]
        public string Name { get; set; }
        [StringLength(50), Required]
        public string MiddleName { get; set; }
        [StringLength(50), Required]
        public string LastName { get; set; }
        public byte[] ProfilePicture { get; set; }
        public string FirebaseId { get; set; }
        public string FirebasePassword { get; set; }
        [StringLength(15)]
        public string AuthValidationCode { get; set; }
        public bool IsActive { get; set; }
    }
}
