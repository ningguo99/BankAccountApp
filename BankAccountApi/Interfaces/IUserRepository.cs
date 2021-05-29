using System.Collections.Generic;
using System.Threading.Tasks;
using BankAccountApi.Entities;

namespace BankAccountApi.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> CreateAsync(AppUser appUser);
        Task<IEnumerable<AppUser>> GetUsersAsync();
        Task<bool> UsernameExists(string username);
    }
}