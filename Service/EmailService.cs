using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;
using LearnAspNetCore.Models;
using LearnAspNetCore.Services;

namespace LearnAspNetCore.Services
{
    public class EmailService : IEmailService
    {
        private readonly SmtpSettings _smtpSettings;

        public EmailService(IOptions<SmtpSettings> smtpSettings)
        {
            _smtpSettings = smtpSettings.Value;
            Console.WriteLine($"DEBUG: SenderEmail is: {_smtpSettings.SenderEmail}");
        }
        public async Task SendEmailAsync(string toEmail, string subject, string body, string? toName = null)
        {
            if (string.IsNullOrWhiteSpace(toEmail))
                {
                    Console.WriteLine("ERROR: toEmail is null or empty!");
                    throw new ArgumentException("Email người nhận không được để trống", nameof(toEmail));
                }
            if (string.IsNullOrWhiteSpace(_smtpSettings.SenderEmail))
                    {
                        Console.WriteLine("ERROR: SenderEmail is null or empty!");
                        throw new InvalidOperationException("Email người gửi chưa được cấu hình");
                    }
            var fromAddress = new MailAddress(_smtpSettings.SenderEmail, _smtpSettings.SenderName);
            var toAddress = new MailAddress(toEmail, toName ?? toEmail);

            try
            {
                using (var message = new MailMessage(fromAddress, toAddress))
                {
                    message.Subject = subject;
                    message.Body = body;
                    message.IsBodyHtml = true;
                    using (var smtp = new SmtpClient(_smtpSettings.Host, _smtpSettings.Port))
                    {
                        smtp.Credentials = new NetworkCredential(_smtpSettings.Username, _smtpSettings.Password);
                        smtp.EnableSsl = true; 
                        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                        smtp.Timeout = 20000;

                        await smtp.SendMailAsync(message);
                        Console.WriteLine($"Email đã gửi thành công đến: {toEmail}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi gửi email đến {toEmail}: {ex.Message}");
            }
    }
}
}
    