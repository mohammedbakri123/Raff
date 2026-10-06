using Raff.Models;

public record createBookDto
{
    public required string title { set; get; }
    public int publishedYear { set; get; }
    public int authorId { set; get; }
}