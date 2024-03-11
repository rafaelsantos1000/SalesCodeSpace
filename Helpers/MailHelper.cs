using MailKit.Net.Smtp;
using MimeKit;
using SalesCodeSpace.Responses;

namespace SalesCodeSpace.Helpers
{
    public class MailHelper : IMailHelper
    {
        private readonly IConfiguration _configuration;

        public MailHelper(IConfiguration configuration)
        {
            _configuration = configuration;

        }

        public Response SendMail(string toName, string toEmail, string subject, string body)
        {
            try
            {
                string from = _configuration["Mail:From"];
                if (from?.Contains("${EMAIL_FROM}") == true)
                {
                    string keys = Environment.GetEnvironmentVariable("EMAIL_FROM");
                    from = from.Replace("${EMAIL_FROM}", keys);
                }

                string name = _configuration["Mail:Name"];
                string smtp = _configuration["Mail:Smtp"];

                string port = _configuration["Mail:Port"];
                if (port?.Contains("${EMAIL_PORT}") == true)
                {
                    string keys = Environment.GetEnvironmentVariable("EMAIL_PORT");
                    port = port.Replace("${EMAIL_PORT}", keys);
                }

                string password = _configuration["Mail:Password"];
                if (password?.Contains("${EMAIL_PASSWORD}") == true)
                {
                    string keys = Environment.GetEnvironmentVariable("EMAIL_PASSWORD");
                    password = password.Replace("${EMAIL_PASSWORD}", keys);
                }

                MimeMessage message = new();
                message.From.Add(new MailboxAddress(name, from));
                message.To.Add(new MailboxAddress(toName, toEmail));
                message.Subject = subject;
                BodyBuilder bodyBuilder = new()
                {
                    HtmlBody = body
                };
                message.Body = bodyBuilder.ToMessageBody();

                using (SmtpClient client = new())
                {
                    client.Connect(smtp, int.Parse(port!), false);
                    client.Authenticate(from, password);
                    client.Send(message);
                    client.Disconnect(true);
                }

                return new Response { IsSuccess = true };

            }
            catch (Exception ex)
            {

                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message,
                    Result = ex
                };
            }

        }
    }
}