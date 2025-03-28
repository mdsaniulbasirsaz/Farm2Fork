using Farm2Fork.Models;
using System.Threading.Tasks;
using Farm2Fork.DTOs;

namespace Farm2Fork.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetUsersAsync();
        Task<User?> RegisterUserAsync(RegisterDto registerDto);
        Task<ProfileDto?> AuthenticateUserAsync(string email, string password);
        Task<User?> GetUserByIdAsync(int id); // Add this line
    }
}
