using Microsoft.EntityFrameworkCore.Storage;
using UserApi.Models.Constants;
using UserApi.Models.Dtos;
using UserApi.Services;

namespace UserApi.Validators
{
    public class UserValidator : IUserValidator
    {
        private readonly IUserService _userService;

        public UserValidator(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<(bool IsValid, string ResponseMessage)> ValidateUserPutRequestAsync(int id, UserDto user)
        {
            if (id != user.Id)
            {
                return (false, ResponseConstants.NoIdMatch);
            }

            var userExists = await _userService.DoesUserExistsWithEmail(user);
            if (userExists)
            {
                return (false, ResponseConstants.EmailInUse);
            }

            return (true, null);
        }

        public async Task<(bool IsValid, string ResponseMessage)> ValidateEmailUniquenessAsync(UserDto user)
        {
            var userExists = await _userService.DoesUserExistsWithEmail(user);
            if (userExists)
            {
                return (false, ResponseConstants.EmailInUse);
            }

            return (true, null);
        }
    }
}
