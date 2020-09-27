using Shared.Web.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Web.Services
{
    public class EmailSender : IEmailSender
    {
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            await SendEmailViaSeS(email, subject, message);
        }

        private async Task SendEmailViaSeS(string toMailAddress, string subject, string body)
        {
            try
            {
                var fromAddress = new MailAddress("frankofoedu@insytai.com", "Frank");
                var toAddress = new MailAddress(toMailAddress, toMailAddress);

                const string fromPassword = "BLbGzt/KtaMqPEkCZhaudJvZa7g0SEWMkNnr+nRqFCOL";


                var smtp = new SmtpClient
                {
                    Host = "email-smtp.us-west-2.amazonaws.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential("AKIAST25HEZU5RIFSQXR", fromPassword)
                };

                using var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                };

                await smtp.SendMailAsync(message);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
     
    }

}
