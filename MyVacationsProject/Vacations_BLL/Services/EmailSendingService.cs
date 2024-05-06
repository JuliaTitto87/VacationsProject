using MimeKit;
using Org.BouncyCastle.Crypto.Macs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vacations_BLL.Contracts;

namespace Vacations_BLL.Services
{
    public class EmailSendingService : IEmailSendingService
    {
        public void SendEmail(string email, string message)
        {
            try
            {
                MimeMessage mimeMessage = new MimeMessage();
                mimeMessage.From.Add(new MailboxAddress("Vacation", "konopatik125@gmail.com"));
                mimeMessage.To.Add(new MailboxAddress("", email));
                mimeMessage.Subject = "Сообщение от VacationProject";
                mimeMessage.Body = new BodyBuilder()
                {
                    HtmlBody = "<div style=\"color:green;\">" + message + "</div>"
                }.ToMessageBody();

                using (MailKit.Net.Smtp.SmtpClient client = new MailKit.Net.Smtp.SmtpClient())
                {
                    client.Connect("stmp.gmail.com", 587, true);
                    client.Authenticate("konopatik125@gmail.com", "Titto_103125");
                    client.Send(mimeMessage);
                    client.Disconnect(true);

                }
            }
            catch { }
        }
    }
}
