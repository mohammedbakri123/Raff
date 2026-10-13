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
    public async Task<BookDto> SetAuthorAsync(BookAuthorDto dto)
    {
        var book = await context.Books.FindAsync(dto.id);
        var author = await context.Authors.FindAsync(dto.authorId);

        if (book == null || author == null)
        {
            throw new KeyNotFoundException($"Book with ID {dto.id} or Author with ID {dto.authorId} not found.");
        }

        book.Authors.Add(author);
        await context.SaveChangesAsync();

        return Map(book);

    }
    public async Task<BookDto> RemoveAuthorAsync(BookAuthorDto dto)
    {
        var book = await context.Books.FindAsync(dto.id);
        var author = await context.Authors.FindAsync(dto.authorId);

        if (book == null || author == null)
        {
            throw new KeyNotFoundException($"Book with ID {dto.id} or Author with ID {dto.authorId} not found.");
        }

        bool isAuthorAssociated = book.Authors.Any(a => a.Id == dto.authorId);
        if (!isAuthorAssociated)
        {
            throw new InvalidOperationException($"Author with ID {dto.authorId} is not associated with Book with ID {dto.id}.");
        }

        book.Authors.Remove(author);
        await context.SaveChangesAsync();

        return Map(book);
    }

    public async Task<BookDto> GetBookByIdAsync(int id)
    {
        var book = await context.Books
            .Include(b => b.Authors)
            .FirstOrDefaultAsync(b => b.Id == id);
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
        var items = await query
        .Include(b => b.Authors)
        .OrderBy(b => b.Id)
        .Skip((page - 1) * pageSize)
        .Take(pageSize)
        .ToListAsync();

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

    public async Task<BookDto> UpdateBookAsync(int id, updateBookDto dto)
    {
        var book = await context.Books.FindAsync(id);

        if (book == null)
        {
            throw new KeyNotFoundException($"Book with ID {id} not found.");
        }

        if (dto.title is not null)
            book.Title = dto.title;

        if (dto.summary is not null)
            book.Summary = dto.summary;

        if (dto.CoverUrl is not null)
            book.CoverUrl = dto.CoverUrl;

        if (dto.publishedYear is not null)
            book.PublishedYear = dto.publishedYear;

        await context.SaveChangesAsync();

        return Map(book);


    }

    public async Task DeleteBookAsync(int id)
    {
        var book = await context.Books.FindAsync(id);

        if (book == null)
        {
            throw new KeyNotFoundException($"Book with ID {id} not found.");
        }

        context.Books.Remove(book);

        await context.SaveChangesAsync();

    }


    private BookDto Map(Book b)
    {
        BookDto book = new BookDto(b.Id, b.Title, b.PublishedYear, b.Summary, b.CoverUrl, b.Authors.Select(a => Map(a)).ToArray());
        return book;

    }

    private AuthorDto Map(Author a)
    {
        AuthorDto author = new AuthorDto(a.Id, a.Name, a.AvatarUrl);
        return author;
    }


}