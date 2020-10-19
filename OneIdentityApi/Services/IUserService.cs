using System.Collections.Generic;
using System.Threading.Tasks;
using OneIdentityApi.Models;

namespace OneIdentityApi.Services
{
    public interface IUserService
    {
        Task<User> GetUserByIdAsync(string id);
        Task<List<User>> GetUsersAsync();
        Task AddUserAsync(User user);
        void UpdateUserAsync(User user);
        void DeleteUserAsync(int userId);
    }
}