using API.Contracts;
using System.Net.Mail;

namespace API.Ultilities.Handler
{
    public class EmailHandler : IEmailHandler
    {
        private readonly string _smptServer;
        private readonly int _smptPort;
        private readonly string _fromEmailAddress;

        public EmailHandler(string smptServer, int smptPort, string fromEmailAddress)
        {
            _smptServer = smptServer;
            _smptPort = smptPort;
            _fromEmailAddress = fromEmailAddress;
        }

        public void sendEmail(string toEmail, string subject, string htmlMessage)
        {
            var message = new MailMessage
            {
                From = new MailAddress(_fromEmailAddress),
                Subject = subject,
                Body = htmlMessage,
                IsBodyHtml = true
            };

            message.To.Add(new MailAddress(toEmail));

            using var client = new SmtpClient(_smptServer, _smptPort);
            client.Send(message);
        }
    }
}
