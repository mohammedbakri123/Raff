
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Raff.Data;
using Raff.DTOs.Auth;
using Raff.DTOs.Users;
using Raff.Models;
using Raff.Domain.Enums;

namespace Raff.Services;

public sealed class AuthService(
    BooksContext context,
    IPasswordHasher<User> passwordHasher) : IAuthService
{
    public async Task<UserDto?> AuthenticateAsync(
        LoginRequest request,
        CancellationToken cancellationToken = default)
    {
        var user = await context.Users
            .SingleOrDefaultAsync(
                u => u.Email == request.Email,
                cancellationToken);

        if (user is null)
            return null;

        var result = passwordHasher.VerifyHashedPassword(
            user,
            user.PasswordHash,
            request.Password);

        if (result == PasswordVerificationResult.Failed)
            return null;

        return new UserDto(
            user.Id,
            user.Username,
            user.Email,
            user.Role,
            user.CreatedAt);
    }

    public async Task<UserDto?> RegisterAsync(
    RegisterRequest request,
    CancellationToken cancellationToken = default)
    {
        var emailExists = await context.Users.AnyAsync(
            u => u.Email == request.Email,
            cancellationToken);

        var usernameExists = await context.Users.AnyAsync(
            u => u.Username == request.Username,
            cancellationToken);

        if (emailExists || usernameExists)
            return null;

        var user = new User
        {
            Username = request.Username,
            Email = request.Email,
            Role = UserRole.Reader,
            CreatedAt = DateTime.UtcNow
        };

        user.PasswordHash = passwordHasher.HashPassword(
            user,
            request.Password);

        context.Users.Add(user);
        await context.SaveChangesAsync(cancellationToken);

        return new UserDto(
            user.Id,
            user.Username,
            user.Email,
            user.Role,
            user.CreatedAt);
    }
}
