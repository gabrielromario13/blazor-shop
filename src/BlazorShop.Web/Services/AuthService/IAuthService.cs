namespace BlazorShop.Web.Services.AuthService;

public interface IAuthService
{
    Task<ServiceResponse<int>> Register(UserRegisterRequest request);
    Task<ServiceResponse<string>> Login(UserLoginRequest request);
    Task<ServiceResponse<bool>> ChangePassword(UserChangePasswordRequest request);
    Task<bool> IsUserAuthenticated();
}