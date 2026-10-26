
using Raff.DTOs.Auth;

namespace Raff.Services;

public interface IAuthService
{
    Task<bool> ValidateCredentialsAsync(LoginRequest request, CancellationToken cancellationToken = default);
}