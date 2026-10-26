
using Raff.Domain.Enums;

namespace Raff.DTOs.Users;

public sealed record UserDto(
    int Id,
    string Username,
    string Email,
    UserRole Role,
    DateTime? CreatedAt);
