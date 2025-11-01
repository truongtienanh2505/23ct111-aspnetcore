namespace LearnAspNetCore.Models
{
    public class SmtpSettings
    {
        public const string SettingName = "SmtpSettings";

        public string Host { get; set; }
        public int Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string SenderEmail { get; set; }
        public string SenderName { get; set; }
    }
}