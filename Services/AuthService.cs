using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Raff.Data;
using Raff.DTOs.Auth;
using Raff.Models;

namespace Raff.Services;

public sealed class AuthService(
    BooksContext context,
    IPasswordHasher<User> passwordHasher)
{
    public async Task<bool> ValidateCredentialsAsync(
        LoginRequest request,
        CancellationToken cancellationToken = default)
    {
        var user = await context.Users
            .SingleOrDefaultAsync(
                u => u.Email == request.Email,
                cancellationToken);

        if (user is null)
            return false;

        var result = passwordHasher.VerifyHashedPassword(
            user,
            user.PasswordHash,
            request.Password);

        return result != PasswordVerificationResult.Failed;
    }
}