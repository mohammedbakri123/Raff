
namespace Raff.Services;

public interface ITokenService
{
    string GenerateAccessToken(int userId, string username, string email);
}
