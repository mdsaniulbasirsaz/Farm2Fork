using Farm2Fork.Models;
using Farm2Fork.Repositories.Interfaces;
using Farm2Fork.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using Farm2Fork.DTOs;
using BCrypt.Net;
using Farm2Fork.Repositories;


namespace Farm2Fork.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        private readonly IOtpRepository _otpRepository;
        private readonly IEmailService _emailService;
        public UserService(IUserRepository userRepository, IOtpRepository otpRepository, IEmailService emailService)
        {
            _userRepository = userRepository;
            _otpRepository = otpRepository;
            _emailService = emailService;

        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await _userRepository.GetAllUsersAsync();
        }

        public async Task<User?> RegisterUserAsync(RegisterDto registerDto)
        {
            try{
                var existingUser = await _userRepository.GetUserByEmailAsync(registerDto.Email);
                if (existingUser != null)
                {
                    throw new InvalidOperationException("Email already in use. Please use a different email address.");
                }

                var hashedPassword = BCrypt.Net.BCrypt.HashPassword(registerDto.Password);
                var user = new User
                {
                    Email = registerDto.Email,
                    Password = hashedPassword,
                    Name = registerDto.Name,
                    UserType = registerDto.UserType,
                    IsVerified = false,
                    CreatedAt = DateTime.UtcNow
                };

                await _userRepository.AddUserAsync(user);

                var otp = GenerateOtp();
                await _emailService.SendOtpEmailAsync(registerDto.Email, otp);

                // Store OTP
                await _otpRepository.SaveOtpAsync(new OtpRecord { Email = registerDto.Email, Otp = otp });

                return user;
            } catch{
                throw new InvalidOperationException("Email already in use. Please use a different email address.");

            }
        }

        private string GenerateOtp()
        {
            var random = new Random();
            var otp = random.Next(100000, 999999).ToString();
            return otp;
        }

        public async Task<bool> VerifyOtpAsync(string email, string otp)
        {
            var storedOtp = await _otpRepository.GetOtpAsync(email);
            if (storedOtp == null)
            {
                
                // Console.WriteLine("OTP not found for email: " + email);
                return false;
            }

            if (storedOtp != otp)
            {
                // Console.WriteLine("OTP mismatch. Provided: " + otp + ", Stored: " + storedOtp);
                return false;
            }

            var user = await _userRepository.GetUserByEmailAsync(email);
            if (user == null)
            {
                Console.WriteLine("User not found for email: " + email);
                return false;
            }

            user.IsVerified = true;
            user.VerifiedAt = DateTime.UtcNow;

            await _userRepository.UpdateUserAsync(user);

            return true;
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
