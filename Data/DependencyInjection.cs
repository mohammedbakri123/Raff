using Microsoft.EntityFrameworkCore;
using Npgsql.NameTranslation;
using Raff.Models;
using Raff.Services;

namespace Raff.Data;

public static class DependencyInjection
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<BooksContext>(options =>
            options.UseNpgsql(
                configuration.GetConnectionString("DefaultConnection"))
                .LogTo(Console.WriteLine, LogLevel.Information)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors());



    
        return services;
    }



    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IBookService, BookService>();
        return services;
    }
}
