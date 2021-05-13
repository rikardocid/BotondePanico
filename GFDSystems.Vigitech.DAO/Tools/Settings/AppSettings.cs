using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GFDSystems.Vigitech.DAO.Tools.Settings
{
    public class AppSettings
    {
        public string ValidAudience { get; set; }
        public string ValidIssuer { get; set; }
        public string IssuerSigningKey { get; set; }
        public string EmailSuperFirebase { get; set; }
        public string PasswordSuperFirebase { get; set; }

    }
}
