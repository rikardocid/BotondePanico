using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using GFDSystems.Vigitech.DAL.Enums;

namespace GFDSystems.Vigitech.DAL.Entities
{
    public class NotificationBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("NotificationBaseId")]
        public int Id { get; set; }
        [Column("Type"), StringLength(6), Required]
        public string Type { get; set; }
        [Column("Title"), StringLength(40), Required]
        public string Title { get; set; }
        [Column("Message"), StringLength(250), Required]
        public string Message { get; set; }
        [Column("LevelPriority")]
        public Priority LevelPriority { get; set; }
        [Column("Status"), StringLength(6), Required]
        public string Status { get; set; }
        [Column("IdAspNetUserSend"), Required]
        public int IdAspNetUserSend { get; set; }
        [Column("IdUserFirebaseSend"), StringLength(30), Required]
        public string IdUserFirebaseSend { get; set; }
        [Column("UserNameSend"), StringLength(50), Required]
        public string UserNameSend { get; set; }
        [Column("IdAspNetUserReceive"), Required]
        public int IdAspNetUserReceive { get; set; }
        [Column("IdUserFirebaseReceive"), StringLength(30), Required]
        public string IdUserFirebaseReceive { get; set; }
        [Column("UserNameReceive"), StringLength(50), Required]
        public string UserNameReceive { get; set; }
        [Column("IsActive"), Required]
        public bool IsActive { get; set; }
        [Column("DateRegister")]
        public DateTime DateRegister { get; set; }
        [Column("IdFirebaseNotification"), StringLength(35)]
        public string IdFirebaseNotification { get; set; }
        [Column("Latitude")]
        public double Latitude { get; set; }
        [Column("Longitude")]
        public double Longitude { get; set; }
    }
}
