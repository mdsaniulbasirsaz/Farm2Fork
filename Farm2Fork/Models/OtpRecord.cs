namespace Farm2Fork.Models
{
    public class OtpRecord
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Otp { get; set; } = string.Empty;
        public DateTime ExpiryTime { get; set; }
    }
}
