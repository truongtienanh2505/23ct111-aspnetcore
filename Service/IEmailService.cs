using System.Threading.Tasks;
using LearnAspNetCore.Services;
using LearnAspNetCore.Models;
namespace LearnAspNetCore.Services
{
    public interface IEmailService
    {
        /// <param name="toEmail
        /// <param name="subject
        /// <param name="body
        /// <param name="toName
        Task SendEmailAsync(string toEmail, string subject, string body, string? toName = null);
    }
}