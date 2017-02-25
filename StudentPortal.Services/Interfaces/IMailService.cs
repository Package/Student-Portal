using StudentPortal.Domain.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace StudentPortal.Services.Interfaces
{
    public interface IMailService
    {
        Task SendEmailAsync(string emailAddress, string subject, string message, string emailTemplate, Dictionary<string, string> parameters);

        void SendEmail(string emailAddress, string subject, string message, string emailTemplate, Dictionary<string, string> parameters);

        //MailMessage ConfigureMailMessage(string emailAddress, string subject, string body);

        //SmtpClient ConfigureClient();
    }
}
