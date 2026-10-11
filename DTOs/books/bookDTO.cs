using Raff.Models;

public record BookDto
{
    public int id { set; get; }

    public required string title { set; get; }

    public int publishedYear { set; get; }

    public int authorId { set; get; }
    public string? Summary { get; set; }

    public string? CoverUrl { get; set; }

    public virtual ICollection<Author> Authors { get; set; } = new List<Author>();

}