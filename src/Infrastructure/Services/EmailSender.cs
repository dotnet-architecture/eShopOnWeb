using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Microsoft.eShopWeb.Infrastructure.Services
{
    // This class is used by the application to send email for account confirmation and password reset.
    // For more details see https://go.microsoft.com/fwlink/?LinkID=532713
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string message)
        {

            System.Threading.CancellationToken cancellationToken = new System.Threading.CancellationToken();

            var mailMessage = new MailMessage();

            mailMessage.To.Add(email);

            mailMessage.From = new MailAddress("mailaddress@username.com");//TODO : You can add mail address 

            mailMessage.Subject = "There is no subject";

            mailMessage.Body = message;
            mailMessage.IsBodyHtml = true;

            using (var smtp = new SmtpClient())
            {
                smtp.UseDefaultCredentials = false;

                var credential = new NetworkCredential
                {
                    UserName = "mailaddress@username.com", //TODO : You can add mail address 
                    Password = "password"                  //TODO : You can add password 
                };

                smtp.Credentials = credential;

                smtp.EnableSsl = true;

                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;

                smtp.Host = "smtp.gmail.com";//TODO :If you want you can add other host,but its work 
                smtp.Port = 587;
                try
                {

                    Task taskAsyncs = smtp.SendMailAsync(mailMessage);
                    mailMessage.DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess;

                    return Task.CompletedTask;
                }
                catch (Exception ex)
                {
                    mailMessage.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

                    return Task.FromCanceled(cancellationToken);

                }
            }


            // TODO: Wire this up to actual email sending logic via SendGrid, local SMTP, etc.
           // return Task.CompletedTask;
        }
    }
}
