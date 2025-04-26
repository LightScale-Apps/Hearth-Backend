using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using api.Dtos.PatientData;
using api.Models;

namespace api.Interfaces
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string message) {
            var addr = "ryanbhillis@gmail.com";
            var pw = "zasy hlrx nwjw igay";

            var client = new SmtpClient("smtp.gmail.com", 587) {
                EnableSsl = true,
                Credentials = new NetworkCredential(addr, pw)
            };
            var emailMessage = new MailMessage(
                from: addr,
                to: email,
                subject,
                message
            );
            emailMessage.IsBodyHtml = true;

            return client.SendMailAsync(emailMessage);
        }

    }
}