using UserApi.Models.Dtos;

namespace UserApi.Validators
{
    public interface IUserValidator
    {
        Task<(bool IsValid, string ResponseMessage)> ValidateUserPutRequestAsync(int id, UserDto user);
        Task<(bool IsValid, string ResponseMessage)> ValidateEmailUniquenessAsync(UserDto user);
    }
}