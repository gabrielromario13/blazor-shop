namespace BlazorShop.Web.Services.AuthService;

public class AuthService(HttpClient client, AuthenticationStateProvider authStateProvider) : IAuthService
{
    private readonly HttpClient _client = client;
    private readonly AuthenticationStateProvider _authStateProvider = authStateProvider;

    public async Task<ServiceResponse<bool>> ChangePassword(UserChangePasswordRequest request)
    {
        var result = await _client.PostAsJsonAsync("api/auth/change-password", request.Password);
        return await result.Content.ReadFromJsonAsync<ServiceResponse<bool>>();
    }

    public async Task<bool> IsUserAuthenticated()
    {
        return (await _authStateProvider.GetAuthenticationStateAsync()).User.Identity.IsAuthenticated;
    }

    public async Task<ServiceResponse<string>> Login(UserLoginRequest request)
    {
        var result = await _client.PostAsJsonAsync("api/auth/login", request);
        return await result.Content.ReadFromJsonAsync<ServiceResponse<string>>();
    }

    public async Task<ServiceResponse<int>> Register(UserRegisterRequest request)
    {
        var result = await _client.PostAsJsonAsync("api/auth/register", request);
        return await result.Content.ReadFromJsonAsync<ServiceResponse<int>>();
    }
}