using System.ComponentModel.DataAnnotations;

public record updateBookDto
{
    [MaxLength(100)]
    public string? title { set; get; }


    [Range(0, 2100)]
    public int? publishedYear { set; get; }


    public string? summary { set; get; }

    public string? CoverUrl { get; set; }


}