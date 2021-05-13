using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GFDSystems.Vigitech.DAO.Models
{
    public class NotificationBaseDAO
    {
        public int Id { get; set; }
        [StringLength(6), Required]
        public string Type { get; set; }
        [StringLength(250), Required]
        public string Message { get; set; }
        [StringLength(6), Required]
        public string Status { get; set; }
        [Required]
        public int IdAspNetUserSend { get; set; }
        [StringLength(30), Required]
        public string IdUserFirebaseSend { get; set; }
        [StringLength(50), Required]
        public string UserNameSend { get; set; }
        [Required]
        public int IdAspNetUserReceive { get; set; }
        [StringLength(30), Required]
        public string IdUserFirebaseReceive { get; set; }
        [StringLength(50), Required]
        public string UserNameReceive { get; set; }
        [Required]
        public bool IsActive { get; set; }
        public DateTime DateRegister { get; set; }
        [StringLength(35)]
        public string IdFirebaseNotification { get; set; }
    }
}
