using UserApi.Data;
using UserApi.Models;

namespace UserApi.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserCache _cache;

        public UserService(IUserRepository userRepository, IUserCache cache) 
        {
            _userRepository = userRepository;
            _cache = cache;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _userRepository.GetAllAsync();
        }

        public async Task<User> GetByIdAsync(int id)
        {
            var user = _cache.Get(id);
            if (user == null)
            {
                user = await _userRepository.GetByIdAsync(id);

                if(user == null)
                {
                    return user;
                }
                _cache.Set(user);
            }

            return user;
        }

        public async Task CreateAsync(User user)
        {
           await _userRepository.CreateAsync(user);
        }

        public async Task<User> DeleteAsync(int id)
        {
            var userDeleted = await _userRepository.DeleteAsync(id);
            if (userDeleted != null)
            {
                _cache.Remove(id);
            }
            return userDeleted;
        }

        public async Task UpdateAsync(User user)
        {
            await _userRepository.UpdateAsync(user);
            _cache.Remove(user.Id);
        }

        public async Task<User> ValidateUser(User user)
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetWithEmailAsync(string email)
        {
            return await _userRepository.GetWithEmailAsync(email);
        }

        public async Task<bool> DoesUserExistsWithEmail(string email)
        {
            var userExists = await _userRepository.DoesUserExistWithEmail(email);
            if (userExists)
            {
                return true;
            }
            return false;
        }
    }
}
