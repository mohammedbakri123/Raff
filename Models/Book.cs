using System;
using System.Collections.Generic;

namespace Riff.Models;

public partial class Book
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public int? PublishedYear { get; set; }

    public string? Summary { get; set; }

    public string? CoverUrl { get; set; }

    public virtual ICollection<ReadingEntry> ReadingEntries { get; set; } = new List<ReadingEntry>();

    public virtual ICollection<Author> Authors { get; set; } = new List<Author>();
}
