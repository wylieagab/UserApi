namespace UserApi.Models
{
    public static class UserMapper
    {
        public static User ToUser(UserDto userDto)
        {
            return new User
            {
                Id = userDto.Id,
                Name = userDto.Name,
                Email = userDto.Email,
                Address = userDto.Address,
                DOB = userDto.DOB
            };
        }

        public static UserDto ToUserDto(User user)
        {
            return new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Address = user.Address,
                DOB = user.DOB
            };
        }
    }
}
