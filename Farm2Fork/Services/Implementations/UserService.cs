using Farm2Fork.Models;
using Farm2Fork.Repositories.Interfaces;
using Farm2Fork.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using Farm2Fork.DTOs;
using BCrypt.Net;

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
                var hashedPassword = BCrypt.Net.BCrypt.HashPassword(registerDto.Password);
                var user = new User
                {
                    Email = registerDto.Email,
                    // Password = registerDto.Password,
                    Password = hashedPassword,
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

        public async Task<ProfileDto?> AuthenticateUserAsync(string email, string password)
        {
            var user = await _userRepository.GetUserByEmailAsync(email);
                if (user == null ||!BCrypt.Net.BCrypt.Verify(password,user.Password)) 
                    return null;

                if (!user.IsVerified) 
                    return null;

                var userDetails = new ProfileDto
                {
                    Name = user.Name,
                    Email = user.Email,
                    UserType = user.UserType,
                    IsVerified = user.IsVerified,
                    CreatedAt = user.CreatedAt
                };

                return userDetails;
        }


        public async Task<User?> GetUserByIdAsync(int id)  
        {
            return await _userRepository.GetUserByIdAsync(id);
        }
    }
}
