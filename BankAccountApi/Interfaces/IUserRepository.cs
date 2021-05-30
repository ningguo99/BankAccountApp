using System.Collections.Generic;
using System.Threading.Tasks;
using BankAccountApi.Entities;

namespace BankAccountApi.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> CreateAsync(AppUser appUser);
        Task<IEnumerable<AppUser>> GetUsersAsync();
        Task<AppUser> GetUserByIdAsync(int id);
        Task<bool> UsernameExists(string username);
        Task<bool> AccountBelongsToUser(int userId, int accountId);
    }
}