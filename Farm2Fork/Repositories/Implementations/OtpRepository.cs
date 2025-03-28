using System.Collections.Generic;
using System.Threading.Tasks;
using Farm2Fork.Data;
using Farm2Fork.Models;
using Microsoft.EntityFrameworkCore; 

namespace Farm2Fork.Repositories
{
    public class OtpRepository : IOtpRepository
    {
        private readonly AppDbContext _context; // Replace with your actual DbContext

        public OtpRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task SaveOtpAsync(OtpRecord otpRecord)
        {
            _context.OtpRecords.Add(otpRecord);
            await _context.SaveChangesAsync();
        }

        public async Task<string?> GetOtpAsync(string email)
        {
            var otpRecord = await _context.OtpRecords
                .FirstOrDefaultAsync(otp => otp.Email == email);

            return otpRecord?.Otp;
        }


        
    }
}
