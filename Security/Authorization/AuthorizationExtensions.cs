using Microsoft.AspNetCore.Authorization;

namespace Raff.Authorization;

public static class AuthorizationExtensions
{
    public static IServiceCollection AddRaffAuthorization(
        this IServiceCollection services)
    {
        services.AddAuthorization(options =>
        {
            options.AddPolicy("CanManageBooks", policy =>
            {
                policy.RequireAuthenticatedUser();
                policy.RequireRole("Admin");
                policy.RequireClaim("permission", "books.write");
            });
        });

        return services;
    }
}