using Raff.Domain.Enums;

public record CreateTokenDTO(
    int UserId,
    string Username,
    string Email,
    UserRole UserRole
    );