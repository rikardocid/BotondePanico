using Firebase.Auth;
using Firebase.Database;
using GFDSystems.Vigitech.DAO.CustomResponse;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GFDSystems.Vigitech.DAO.Firebase
{
    public class FirebaseConfigDAO : IFirebaseConfigDAO
    {
        private readonly FirebaseAuthProvider _authProvider;
        private readonly string _apiKey;
        private readonly string _urlPoyect;

        private FirebaseClient _firebaseClient;

        public FirebaseConfigDAO(IConfiguration configuration)
        {
            _authProvider = new FirebaseAuthProvider(new FirebaseConfig(_apiKey));
            //_firebaseClient = new FirebaseClient(_urlPoyect);
            _apiKey = configuration.GetValue<string>("FirebaseConfig:apiKey");
            _urlPoyect = configuration.GetValue<string>("FirebaseConfig:urlProyect");
        }

        public async Task<FirebaseAuthLink> CreateUser(string Email, string Password, string Name)
        {
            return await _authProvider.CreateUserWithEmailAndPasswordAsync(Email, Password, Name);
        }

        public async Task<FirebaseAuthLink> LogginUser(string pEmail, string pPassword)
        {
            var loginFirebase = await _authProvider.SignInWithEmailAndPasswordAsync(pEmail, pPassword);
            _firebaseClient = new FirebaseClient(_urlPoyect, new FirebaseOptions
            {
                AuthTokenAsyncFactory = () => Task.FromResult(loginFirebase.FirebaseToken)
            });
            return loginFirebase;
        }

        public Task<StatusResponse> SendeNotification(string pEmail, string pPassword, NotificationBaseDAO notification)
        {
            var response = LogginUser(pEmail, pPassword);
            var date = DateTime.Now;
            string currentDate = date.ToString("yyyy-MM-dd");
            string time = date.ToString("hh:mm:ss");
            BaseNotificationFirebase datFirebase = new BaseNotificationFirebase()
            {
                Date = currentDate,
                Time = time,
                IsViewed = false,
                Title = notification.Title,
                Message = notification.Message,
                TypeNotification = notification.Type,
                Priority = notification.LevelPriority,
                UserSenderName = notification.UserNameSend,
                IdNotificationBase = notification.Id,
                Latitude = notification.Latitude,
                Longitude = notification.Longitude
            };
            return null;
        }
    }
}
