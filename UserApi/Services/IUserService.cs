using UserApi.Models.Dtos;

namespace UserApi.Services
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAllAsync();
        Task<UserDto> GetByIdAsync(int id);
        Task<UserDto> CreateAsync(UserDto userDto);
        Task UpdateAsync(UserDto userDto);
        Task<UserDto> DeleteAsync(int id);
        Task<UserDto> GetWithEmailAsync(UserDto userDto);
        Task<bool> DoesUserExistsWithEmail(UserDto userDto);
    }
}
