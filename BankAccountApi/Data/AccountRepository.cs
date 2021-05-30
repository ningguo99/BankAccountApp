using System;
using System.Text;
using System.Threading.Tasks;
using BankAccountApi.Entities;
using BankAccountApi.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BankAccountApi.Data
{
    public class AccountRepository : IAccountRepository
    {
        private readonly DataContext _context;

        public AccountRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> AccountNumberExists(string accountNumber)
        {
            return await _context.BankAccounts.AnyAsync(account => account.AccountNumber == accountNumber);
        }

        public async Task<bool> CreateAsync(BankAccount bankAccount)
        {
            _context.Entry(bankAccount).State = EntityState.Added;
            var success = await _context.SaveChangesAsync() > 0;
            return true;
        }

        public async Task<string> GenerateAccountNumber()
        {
            var builder = new StringBuilder();
            var random = new Random();
            // Generate a random 16-digit account number. If it already exists in DB, re-generate.
            do
            {
                for (int i = 0; i < 16; i++)
                {
                    builder.Append(random.Next(0, 10));
                }
            } while (await AccountNumberExists(builder.ToString()));

            return builder.ToString();
        }

        public async Task<BankAccount> GetBankAccountByIdAsync(int id)
        {
            return await _context.BankAccounts.FindAsync(id);
        }

        public async Task<bool> UpdateAsync(BankAccount bankAccount)
        {
            _context.Entry(bankAccount).State = EntityState.Modified;
            var success = await _context.SaveChangesAsync() > 0;
            return success;
        }
    }
}