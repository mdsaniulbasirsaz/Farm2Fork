using Farm2Fork.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Farm2Fork.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User?> GetUserByIdAsync(int id); // Make this nullable
        Task<User?> GetUserByEmailAsync(string email); // Make this nullable
        Task AddUserAsync(User user);
        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(int id);
    }
}
