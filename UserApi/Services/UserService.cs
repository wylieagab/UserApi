using UserApi.Data;
using UserApi.Models.Dtos;
using UserApi.Models.Mappers;

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

        public async Task<IEnumerable<UserDto>> GetAllAsync()
        {
            var users = await _userRepository.GetAllAsync();
            return users.Select(UserMapper.ToUserDto).ToList();
        }

        public async Task<UserDto> GetByIdAsync(int id)
        {
            var user = _cache.Get(id);
            if (user == null)
            {
                user = await _userRepository.GetByIdAsync(id);

                if(user == null)
                {
                    return null;
                }
                _cache.Set(user);
            }

            return UserMapper.ToUserDto(user);
        }

        public async Task<UserDto> CreateAsync(UserDto userDto)
        {
           var user = UserMapper.ToUser(userDto);
           return UserMapper.ToUserDto(await _userRepository.CreateAsync(user));
        }

        public async Task<UserDto> DeleteAsync(int id)
        {
            var userDeleted = await _userRepository.DeleteAsync(id);
            if (userDeleted != null)
            {
                _cache.Remove(id);
                return UserMapper.ToUserDto(userDeleted);
            }
            return null;
        }

        public async Task UpdateAsync(UserDto userDto)
        {
            var user = UserMapper.ToUser(userDto);
            await _userRepository.UpdateAsync(user);
            _cache.Remove(user.Id);
        }

        public async Task<UserDto> GetWithEmailAsync(UserDto userDto)
        {
            var user = UserMapper.ToUser(userDto);
            return UserMapper.ToUserDto(await _userRepository.GetWithEmailAsync(user.Email));
        }

        public async Task<bool> DoesUserExistsWithEmail(UserDto userDto)
        {
            var user = UserMapper.ToUser(userDto);
            var userExists = await _userRepository.DoesUserExistWithEmail(user.Email);
            if (userExists)
            {
                return true;
            }
            return false;
        }
    }
}
