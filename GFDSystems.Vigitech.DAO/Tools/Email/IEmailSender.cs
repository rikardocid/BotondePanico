using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GFDSystems.Vigitech.DAO.Tools.Email
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string emailSend, string Body, string Asunto = "");
    }
}
