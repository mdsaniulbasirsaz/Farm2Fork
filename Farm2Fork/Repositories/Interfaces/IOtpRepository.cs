using Farm2Fork.Models;

namespace Farm2Fork.Repositories
{
    public interface IOtpRepository
    {
        Task SaveOtpAsync(OtpRecord otpRecord);
        Task<string?> GetOtpAsync(string email);
    }
}
