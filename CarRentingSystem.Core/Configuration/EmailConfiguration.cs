

namespace CarRentingSystem.Configuration
{
    public class EmailConfiguration
    {
        public string SmtpServer { get; set; } = null!;
        public int Port { get; set; }
        public bool EnableSsl { get; set; }
        public string FromAddress { get; set; } = null!;
    }
}
