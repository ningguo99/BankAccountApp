using System.Threading.Tasks;
using BankAccountApi.Entities;

namespace BankAccountApi.Interfaces
{
    public interface IAccountRepository
    {
        Task<bool> CreateAsync(BankAccount bankAccount);
        Task<BankAccount> GetBankAccountByIdAsync(int id);
        Task<bool> UpdateAsync(BankAccount bankAccount);
        Task<bool> AccountNumberExists(string accountNumber);
        Task<string> GenerateAccountNumber();

    }
}