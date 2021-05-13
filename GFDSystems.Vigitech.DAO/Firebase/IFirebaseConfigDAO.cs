using Firebase.Auth;
using GFDSystems.Vigitech.DAO.CustomResponse;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GFDSystems.Vigitech.DAO.Firebase
{
    public interface IFirebaseConfigDAO
    {
        Task<FirebaseAuthLink> CreateUser(string pEmail, string pPassword, string pName);
        Task<FirebaseAuthLink> LogginUser(string pEmail, string pPassword);
        Task<StatusResponse> SendeNotification(string pEmail, string pPassword, NotificationBaseDAO notification);
    }
}
