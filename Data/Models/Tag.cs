namespace Raff.Models;

public class Tag
{
    public int Id { get; set; }

    public required string Name { get; set; }

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();

}