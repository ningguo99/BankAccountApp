using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankAccountApi.Entities;
using BankAccountApi.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BankAccountApi.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> AccountBelongsToUser(int userId, int accountId)
        {
            var user = await _context.AppUsers.Include(user => user.BankAccounts).SingleOrDefaultAsync(user => user.Id == userId);
            return user.BankAccounts.Any(account => account.Id == accountId);
        }

        public async Task<bool> CreateAsync(AppUser appUser)
        {
            _context.Entry(appUser).State = EntityState.Added;
            var success = await _context.SaveChangesAsync() > 0;
            return true;
        }

        public async Task<IEnumerable<AppUser>> GetUsersAsync()
        {
            return await _context.AppUsers.Include(user => user.BankAccounts).ToListAsync();
        }

        public async Task<bool> UsernameExists(string username)
        {
            return await _context.AppUsers.AnyAsync(x => x.Username.ToLower() == username.ToLower());
        }
    }
}