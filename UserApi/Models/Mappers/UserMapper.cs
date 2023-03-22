using UserApi.Models.Dtos;
using UserApi.Models.Entities;
using UserApi.Models.Mappers;

namespace UserApi.Models.Mappers
{
    public static class UserMapper
    {
        public static User ToUser(this UserDto userDto)
        {
            return new User
            {
                Id = userDto.Id,
                Name = userDto.Name,
                Email = userDto.Email,
                Address = userDto.Address?.ToAddress(),
                DOB = userDto.DOB
            };
        }

        public static UserDto ToUserDto(this User user)
        {
            return new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Address = user.Address?.ToAddressDto(),
                DOB = user.DOB
            };
        }
    }
}
