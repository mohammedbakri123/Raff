using System.ComponentModel.DataAnnotations;
using Raff.Models;

public record createBookDto
{
    [Required]
    [MaxLength(100)]
    public required string title { set; get; }


    [Range(0, 2100)]
    public int publishedYear { set; get; }


    public virtual ICollection<Author> Authors { get; set; } = new List<Author>();
}