using System.Net;
using System.Net.Mail;

namespace CursoYoutubeProgramadorTech.Helper
{
    public class Email : IEmail
    {
        private readonly IConfiguration _configuration;

        public Email(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public bool Send(string email, string title, string message)
        {
            try
            {
                string host = _configuration.GetValue<string>("SMTP:Host");
                string username = _configuration.GetValue<string>("SMTP:Username");
                string name = _configuration.GetValue<string>("SMTP:Name");
                string password = _configuration.GetValue<string>("SMTP:Password");
                int port = _configuration.GetValue<int>("SMTP:Port");

                MailMessage mail = new MailMessage() 
                { 
                    From = new MailAddress(username, name)
                
                };

                mail.To.Add(email);
                mail.Subject = title;
                mail.Body = message;
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.High;

                using (SmtpClient smtp = new SmtpClient(host, port))
                {
                    smtp.Credentials = new NetworkCredential(username, password);
                    smtp.EnableSsl = true;

                    smtp.Send(mail);
                    return true;
                }

            }
            catch (Exception error)
            {
                return false;
            }
        }
    }
}
