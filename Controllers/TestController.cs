using Microsoft.AspNetCore.Mvc;
using LearnAspNetCore.Services;

namespace LearnAspNetCore.Controllers 
{    public class TestController : Controller 
    {
        private readonly IEmailService _emailService;

        public TestController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpGet("send-test-email")]
        public async Task<IActionResult> SendTestEmail()
        {
            string recipient = "hoanghung@gmail.com"; 
            string subject = "Thử nghiệm Gửi Mail thành công!";
            string body = "<h2>Mail đã được gửi từ ứng dụng ASP.NET Core của bạn.</h2><p>Bằng cách sử dụng User Secrets và DI.</p>";

            await _emailService.SendEmailAsync("hoanghunglhu@gmail.com", "test", "body","Name");

            return Ok($"Đã thử gửi email đến");
        }
    }
}