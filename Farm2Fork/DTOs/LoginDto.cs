using Farm2Fork.Models;
namespace Farm2Fork.DTOs
{
    public class LoginDto
    {
        public required string Email {
            get;
            set;
        }

        public required string Password {
            get;
            set;
        }
    }    
}