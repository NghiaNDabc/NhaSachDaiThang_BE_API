using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace NhaSachDaiThang_BE_API.Helper
{
    public class EmailHelper
    {
        private readonly string _smtpServer = "smtp.gmail.com";
        private readonly int _smtpPort = 587;
        private readonly string _senderEmail = "nhasachdaithang@gmail.com"; 
        private readonly string _appPassword = "rnmpzminsnlvnzdk"; 

        public async Task SendEmailAsync(string recipientEmail, string subject, string messageBody)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_senderEmail));
            email.To.Add(MailboxAddress.Parse(recipientEmail));
            email.Subject = subject;

            // Tạo nội dung email
            var builder = new BodyBuilder { HtmlBody = messageBody };
            email.Body = builder.ToMessageBody();

            using var smtp = new SmtpClient();
            try
            {
                smtp.Connect(_smtpServer, _smtpPort, SecureSocketOptions.StartTls);
                smtp.Authenticate(_senderEmail, _appPassword);

                await smtp.SendAsync(email);
                smtp.Disconnect(true);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi gửi email: {ex.Message}");
                throw;
            }
        }
    }
}
