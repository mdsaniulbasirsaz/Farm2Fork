using Microsoft.EntityFrameworkCore;
using Farm2Fork.Models;

namespace Farm2Fork.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users {get; set;}
        public DbSet<OtpRecord> OtpRecords { get; set; }
        // Define your DbSets here. For example:
        // public DbSet<Product> Products { get; set; }
    }
}