
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class BookController : ControllerBase
{
    private readonly IBookService _bookService;

    public BookController(IBookService bookService)
    {
        _bookService = bookService;
    }

    [HttpGet]
    public async Task<IActionResult> GetBooks(int page = 1, int pageSize = 10)
    {
        var result = await _bookService.GetBooksAsync(page, pageSize);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetBook(int id)
    {
        var book = await _bookService.GetBookByIdAsync(id);
        return Ok(book);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] createBookDto dto)
    {
        var book = await _bookService.CreateBookAsync(dto);

        return Ok(book);
    }

    [HttpPatch("setAuthor")]
    public async Task<IActionResult> SetAuthor([FromBody] BookAuthorDto dto)
    {
        var book = await _bookService.SetAuthorAsync(dto);
        return Ok(book);
    }
    [HttpPatch("removeAuthor")]
    public async Task<IActionResult> RemoveAuthor([FromBody] BookAuthorDto dto)
    {
        var book = await _bookService.RemoveAuthorAsync(dto);
        return Ok(book);
    }


    [HttpPatch("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] updateBookDto dto)
    {
        var book = await _bookService.UpdateBookAsync(id, dto);

        return Ok(book);

    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBook(int id)
    {
        try
        {
            await _bookService.DeleteBookAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }
}