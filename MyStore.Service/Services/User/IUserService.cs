using MyStore.Database;
using MyStore.Database.Models.Authentication.Login;
using MyStore.Database.Models.Authentication.SignUp;
using MyStore.Database.Models.User;
using MyStore.Service.Models;

namespace MyStore.Service.Services.User
{
    public interface IUserService
    {
        Task<ResponseMessage<CreateUserResponse>> CreateAsync(RegisterUserModel registerUser);
        Task<ResponseMessage<List<string>>> AssignRoleAsync(List<string> roles, ApplicationUser user);
        Task<ResponseMessage<LoginResponse>> GetJwtTokenAsync(ApplicationUser user);
        Task<ResponseMessage<LoginResponse>> LoginAsync(LoginModel loginModel);
        Task<ResponseMessage<LoginResponse>> RenewAccessTokenAsync(LoginResponse tokens);
    }
}
