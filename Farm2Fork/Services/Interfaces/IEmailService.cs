namespace Farm2Fork.Repositories
{
    public interface IEmailService
    {
        Task SendOtpEmailAsync(string email, string otp);
    }
}
