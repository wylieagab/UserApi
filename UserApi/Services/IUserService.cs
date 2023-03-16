using UserApi.Models;

namespace UserApi.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task<User> GetByIdAsync(int id);
        Task CreateAsync(User user);
        Task UpdateAsync(User user);
        Task<User> DeleteAsync(int id);
        Task<User> GetWithEmailAsync(string email);
        Task<bool> DoesUserExistsWithEmail(string email);
    }
}
