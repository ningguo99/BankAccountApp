using System.Threading.Tasks;
using BankAccountApi.Entities;

namespace BankAccountApi.Interfaces
{
    public interface IAccountRepository
    {
        Task<bool> Create(BankAccount bankAccount);
        Task<bool> AccountNumberExists(string accountNumber);
        Task<string> GenerateAccountNumber();
    }
}