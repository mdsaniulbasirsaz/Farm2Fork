namespace Farm2Fork
{
    public class ProfileDto
    {
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string UserType {get; set;}
        public bool IsVerified {get; set;}
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    }
    
}