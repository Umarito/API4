using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class BooksController(IBookService bookService) : ControllerBase
{
    [HttpPost]
    public async Task<Response<string>> AddAsync(BookDto book)
    {
        return await bookService.AddAsync(book);
    }
    [HttpPut]
    public async Task<Response<string>> UpdateAsync(BookUpdateDto book)
    {
        return await bookService.UpdateAsync(book);
    }
    [HttpDelete]
    public async Task<Response<string>> DeleteAsync(int bookId)
    {
        return await bookService.DeleteAsync(bookId);
    }
    [HttpGet]
    public async Task<List<Book>> GetBooksAsync()
    {
        return await bookService.GetBooksAsync();
    }
    
    [HttpGet("{bookId}")]
    public async Task<Response<Book>> GetBookByIdAsync(int bookId)
    {
        return await bookService.GetBookByIdAsync(bookId);
    }
}