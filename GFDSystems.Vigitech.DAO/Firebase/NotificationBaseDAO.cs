using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using GFDSystems.Vigitech.DAL.Enums;

namespace GFDSystems.Vigitech.DAO.Firebase
{
    public class NotificationBaseDAO
    {
        public int Id { get; set; }
        [StringLength(6), Required]
        public string Type { get; set; }
        [StringLength(40), Required]
        public string Title { get; set; }
        [StringLength(250), Required]
        public string Message { get; set; }
        public Priority LevelPriority { get; set; }
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
        [DataType(DataType.DateTime)]
        public DateTime DateRegister { get; set; }
        [StringLength(35)]
        public string IdFirebaseNotification { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
