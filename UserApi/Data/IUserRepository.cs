using UserApi.Models;

namespace UserApi.Data
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task<User> GetByIdAsync(int id);
        Task<User> CreateAsync(User user);
        Task UpdateAsync(User user);
        Task<User> DeleteAsync(int id);
        Task<User> GetWithEmailAsync(string email);
        Task<bool> DoesUserExistWithEmail(string email);
    }
}
