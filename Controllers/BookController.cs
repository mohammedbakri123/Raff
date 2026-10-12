
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
}