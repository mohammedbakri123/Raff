using Microsoft.AspNetCore.Identity.Data;

public interface IAuthService
{
    Task<bool> ValidateCredentialsAsync(LoginRequest request, CancellationToken cancellationToken = default);
}