using Farm2Fork.Models;
using Farm2Fork.Repositories.Interfaces;
using Farm2Fork.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using Farm2Fork.DTOs;

namespace Farm2Fork.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await _userRepository.GetAllUsersAsync();
        }

        public async Task<User?> RegisterUserAsync(RegisterDto registerDto)
        {
            try{
                var user = new User
                {
                    Email = registerDto.Email,
                    Password = registerDto.Password,
                    Name = registerDto.Name,
                    UserType = registerDto.UserType,
                    IsVerified = false,
                    CreatedAt = DateTime.UtcNow
                };
            await _userRepository.AddUserAsync(user);
            return user;
            } catch{
                throw new InvalidOperationException("Email already in use. Please use a different email address.");

            }
        }

        public async Task<User?> AuthenticateUserAsync(string email, string password)
        {
            var user = await _userRepository.GetUserByEmailAsync(email);
            if (user == null || user.Password != password) return null;
            return user;
        }

        public async Task<User?> GetUserByIdAsync(int id)  // Add this method
        {
            return await _userRepository.GetUserByIdAsync(id);
        }
    }
}
