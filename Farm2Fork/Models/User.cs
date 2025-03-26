namespace Farm2Fork.Models
{
    public class User
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Email {get; set;}
        public required string Password { get; set; }
        public required string UserType {get; set;}
        public bool IsVerified {get; set;}
        public string? OtpCode {get; set;}
        public DateTime? OtpExpiry { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? VerifiedAt { get; set; }
    }

    public enum UserType
    {
        Admin,
        Farm,
        Shop,
        User,
    }
}