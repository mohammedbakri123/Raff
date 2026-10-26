
namespace Raff.Services;

public interface ITokenService
{
    string GenerateAccessToken(CreateTokenDTO tokenDTO);
}
