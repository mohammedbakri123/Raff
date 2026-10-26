
using System.ComponentModel.DataAnnotations;

namespace Raff.DTOs.Auth;

public sealed record RegisterRequest(
    [Required, StringLength(50, MinimumLength = 3)]
    string Username,

    [Required, EmailAddress]
    string Email,

    [Required, MinLength(8), MaxLength(128)]
    string Password
);
