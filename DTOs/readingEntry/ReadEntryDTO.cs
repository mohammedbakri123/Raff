using Raff.DTOs.Users;
using Raff.Domain.Enums;

public record ReadEntryDTO
{
    public int Id { get; init; }

    public BookDto Book { get; init; } = new BookDto(0, null, null, null, null, Array.Empty<AuthorDto>());

    public UserDto User { get; init; } = new UserDto(0, string.Empty, string.Empty, UserRole.Reader, null);

    public DateTime DateRead { get; init; }
    public string Notes { get; init; } = string.Empty;
}