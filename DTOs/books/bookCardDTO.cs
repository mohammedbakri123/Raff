
public record BookCardDto(
    int Id,
    string Title,
    List<string> AuthorNames,
    List<string> Tags,
    int ReadingEntryCount
    );
