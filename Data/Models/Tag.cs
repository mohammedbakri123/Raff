namespace Raff.Models;

public class Tag
{
    public int Id { get; set; }

    public required string Name { get; set; }

    public virtual ICollection<Tag> Tags { get; set; } = new List<Tag>();


}