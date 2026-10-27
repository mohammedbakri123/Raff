using Raff.Models;

// public record BookDto
// {
//     public int id { set; get; }

//     public string? title { set; get; }

//     public int publishedYear { set; get; }

//     public string? Summary { get; set; }

//     public string? CoverUrl { get; set; }


// }

public record BookDto(int id, string? title, int? publishedYear, string? summary, string? coverUrl, AuthorDto?[] Authors);