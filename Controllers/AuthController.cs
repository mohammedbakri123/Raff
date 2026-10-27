
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Raff.DTOs.Auth;
using Raff.Services;

namespace Raff.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(
    IAuthService authService,
    ITokenService tokenService) : ControllerBase
{
    [HttpPost("login")]
    public async Task<IActionResult> Login(
        LoginRequest request,
        CancellationToken cancellationToken)
    {
        var user = await authService.AuthenticateAsync(
            request,
            cancellationToken);

        if (user is null)
            return Unauthorized(new
            {
                message = "Invalid email or password."
            });

        var token = tokenService.GenerateAccessToken(
            new CreateTokenDTO(
                user.Id,
                user.Username,
                user.Email,
                user.Role));

        return Ok(new
        {
            accessToken = token,
            tokenType = "Bearer",
            expiresIn = 900
        });
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(
    RegisterRequest request,
    CancellationToken cancellationToken)
    {
        var user = await authService.RegisterAsync(
            request,
            cancellationToken);

        if (user is null)
            return Conflict(new
            {
                message = "Email or username is already registered."
            });

        return Created($"/api/users/{user.Id}", user);
    }


    [Authorize]
    [HttpGet("me")]
    public IActionResult Me()
    {
        return Ok(new
        {
            Id = User.FindFirst(
                System.Security.Claims.ClaimTypes.NameIdentifier)?.Value,

            Username = User.Identity?.Name,

            Role = User.FindFirst(
                System.Security.Claims.ClaimTypes.Role)?.Value
        });
    }
}
