public interface IBookService
{
    Task<BookDto> CreateBookAsync(createBookDto bookDto);
    Task<BookDto> GetBookByIdAsync(int id);

    Task<BookDto> SetAuthorAsync(BookAuthorDto dto);
    Task<BookDto> RemoveAuthorAsync(BookAuthorDto dto);
    Task<PagedResult<BookDto>> GetBooksAsync(int page, int pageSize);
    Task<BookDto> UpdateBookAsync(int id, updateBookDto bookDto);
    Task<bool> DeleteBookAsync(int id);
}