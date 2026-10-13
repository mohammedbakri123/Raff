using Microsoft.EntityFrameworkCore;
using Raff.Data;
using Raff.Models;

namespace Raff.Services;

public class BookService(BooksContext context) : IBookService
{


    public async Task<BookDto> CreateBookAsync(createBookDto bookDto)
    {


        Book obj = new Book
        {
            Title = bookDto.title,
            PublishedYear = bookDto.publishedYear,
            Summary = bookDto.summary,
            CoverUrl = bookDto.CoverUrl,
            Authors = []

        };
        context.Books.Add(obj);
        await context.SaveChangesAsync();

        return Map(obj);


    }

    public async Task<BookDto> SetAuthorAsync(int id, int authorId)
    {
        var book = await context.Books.FindAsync(id);
        var author = await context.Authors.FindAsync(authorId);

        if (book == null || author == null)
        {
            throw new KeyNotFoundException($"Book with ID {id} or Author with ID {authorId} not found.");
        }

        book.Authors.Add(author);
        await context.SaveChangesAsync();

        return Map(book);


    }

    public async Task<BookDto> GetBookByIdAsync(int id)
    {
        var book = await context.Books.FindAsync(id);
        if (book == null)
        {
            throw new KeyNotFoundException($"Book with ID {id} not found.");
        }
        return Map(book);
    }

    public async Task<PagedResult<BookDto>> GetBooksAsync(int page, int pageSize)
    {
        if (page <= 0 || pageSize <= 0)
        {
            throw new ArgumentException("Page and PageSize must be greater than zero.");
        }
        var query = context.Books.AsNoTracking();

        var totalCount = await query.CountAsync();
        var items = await query.OrderBy(b => b.Id).Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

        PagedResult<BookDto> result = new PagedResult<BookDto>
        {
            Items = items.Select(b => Map(b)),
            TotalCount = totalCount,
            TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize),
            Page = page,
            PageSize = pageSize

        };


        return result;
    }

    public Task<BookDto> UpdateBookAsync(int id, createBookDto bookDto)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteBookAsync(int id)
    {
        throw new NotImplementedException();
    }


    private BookDto Map(Book b)
    {
        BookDto book = new BookDto(b.Id, b.Title, b.PublishedYear, b.Summary, b.CoverUrl);
        return book;

    }

}