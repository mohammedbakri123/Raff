
using Raff.DTOs.Auth;
using Raff.DTOs.Users;

namespace Raff.Services;

public interface IAuthService
{
    Task<UserDto?> AuthenticateAsync(
        LoginRequest request,
        CancellationToken cancellationToken = default);

    Task<UserDto?> RegisterAsync(
RegisterRequest request,
CancellationToken cancellationToken = default);
}
