namespace Raff.Services;

public class BookService : IBookService
{
    public Task<BookDto> CreateBookAsync(createBookDto bookDto)
    {
        throw new NotImplementedException();
    }

    public Task<BookDto> GetBookByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<PagedResult<BookDto>> GetBooksAsync(int page, int pageSize)
    {
        throw new NotImplementedException();
    }

    public Task<BookDto> UpdateBookAsync(int id, createBookDto bookDto)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteBookAsync(int id)
    {
        throw new NotImplementedException();
    }
}