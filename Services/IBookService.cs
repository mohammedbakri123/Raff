public interface IBookService
{
    Task<BookDto> CreateBookAsync(createBookDto bookDto);
    Task<BookDto> GetBookByIdAsync(int id);

    Task<BookDto> SetAuthorAsync(int id, int authorId);
    Task<PagedResult<BookDto>> GetBooksAsync(int page, int pageSize);
    Task<BookDto> UpdateBookAsync(int id, createBookDto bookDto);
    Task<bool> DeleteBookAsync(int id);
}