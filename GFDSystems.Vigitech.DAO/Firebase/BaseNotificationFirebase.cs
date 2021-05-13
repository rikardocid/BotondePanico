using System.ComponentModel.DataAnnotations.Schema;
using GFDSystems.Vigitech.DAL.Enums;
using GFDSystems.Vigitech.DAO.Tools.Extensions;

namespace GFDSystems.Vigitech.DAO.Firebase
{
    public class BaseNotificationFirebase
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public string TypeNotification { get; set; }
        public string LevelPriority {
            get { return Priority.ToString(); }
            private set { Priority = value.ParseEnum<Priority>(); }
        }
        public bool IsViewed { get; set; }
        public string UserSenderName { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public int IdNotificationBase { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        [NotMapped]
        public Priority Priority { get; set; }
    }
}
