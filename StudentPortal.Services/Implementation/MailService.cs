using Postal;
using StudentPortal.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using StudentPortal.Domain.Context;
using StudentPortal.ViewModels;

namespace StudentPortal.Services.Implementation
{
    public class MailService : IMailService
    {
        /// <summary>
        /// Send an asynchronous email message to the provided email address using the provided subject and email template.
        /// </summary>
        /// <param name="emailAddress"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        public async Task SendEmailAsync(string emailAddress, string subject, string message, string emailTemplate, Dictionary<string, string> parameters)
        {
            dynamic email = new Email(emailTemplate);
            email.To = emailAddress;
            email.Subject = subject;
            email.Message = message;
            email.Params = parameters;
            await email.SendAsync();
        }

        /// <summary>
        /// Send an email message to the provided email address using the provided subject and email template.
        /// </summary>
        /// <param name="emailAddress"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        public void SendEmail(string emailAddress, string subject, string message, string emailTemplate, Dictionary<string, string> parameters)
        {
            dynamic email = new Email(emailTemplate);
            email.To = emailAddress;
            email.Subject = subject;
            email.Message = message;
            email.Params = parameters;
            email.Send();
        }

        /// <summary>
        /// Configure the email message, e.g setup who it's being sent to, who it's being sent by, the subject and body.
        /// </summary>
        /// <param name="emailAddress"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        //public MailMessage ConfigureMailMessage(string emailAddress, string subject, string body)
        //{
        //    MailMessage mailMessage = new MailMessage();
        //    mailMessage.From = new MailAddress("administrator@studentportal.com");
        //    mailMessage.IsBodyHtml = true;
        //    mailMessage.To.Add(emailAddress);
        //    mailMessage.Body = body;
        //    mailMessage.Subject = subject;

        //    return mailMessage;
        //}

        /// <summary>
        /// Configure the SMTP client used to send the email message.
        /// </summary>
        /// <returns></returns>
        //public SmtpClient ConfigureClient()
        //{
        //    SmtpClient client = new SmtpClient();
        //    client.Host = "localhost";

        //    return client;
        //}
    }
}
