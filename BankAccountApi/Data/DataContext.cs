using BankAccountApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace BankAccountApi.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<AppUser> AppUsers { get; set; }
    }
}