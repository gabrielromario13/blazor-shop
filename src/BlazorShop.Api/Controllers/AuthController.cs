using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BlazorShop.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController(IAuthService authService) : ControllerBase
{
    private readonly IAuthService _authService = authService;

    [HttpPost("register")]
    public async Task<ActionResult<ServiceResponse<int>>> Register(UserRegisterRequest request)
    {
        var response = await _authService.Register(
            new User
            {
                Email = request.Email
            },
            request.Password);

        return !response.Success
            ? BadRequest(response)
            : Ok(response);
    }

    [HttpPost("login")]
    public async Task<ActionResult<ServiceResponse<string>>> Login(UserLoginRequest request)
    {
        var response = await _authService.Login(request.Email, request.Password);

        return !response.Success
            ? BadRequest(response)
            : Ok(response);
    }

    [HttpPost("change-password")]
    public async Task<ActionResult<ServiceResponse<bool>>> ChangePassword([FromBody] string newPassword)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var response = await _authService.ChangePassword(int.Parse(userId), newPassword);

        return !response.Success
            ? BadRequest(response)
            : Ok(response);
    }
}