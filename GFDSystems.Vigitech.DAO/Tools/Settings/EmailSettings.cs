using System;
using System.Collections.Generic;
using System.Text;

namespace GFDSystems.Vigitech.DAO.Tools.Settings
{
    public class EmailSettings
    {
        public string SmtpServer { get; set; }
        public int SmtpPort { get; set; }
        public bool SmtpServerEnabledSsl { get; set; }
        public string Account { get; set; }
        public string Password { get; set; }
        public string SenderName { get; set; }
        public string SenderEmail { get; set; }
    }
}
