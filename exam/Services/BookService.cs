using System.Net;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.VisualBasic;

public class BookService(ApplicationDBContext dBContext, ILogger<BookService> logger) : IBookService
{
    private readonly ILogger<BookService> _logger = logger;
    private readonly ApplicationDBContext context = dBContext;
    public async Task<Response<string>> AddAsync(BookDto bookDto)
    {
        _logger.LogInformation("Starting the process of adding book...");
        var book = new Book()
        {
            Title = bookDto.Title,
            PublishedYear = bookDto.PublishedYear,
            Genre = bookDto.Genre,
            AuthorId = bookDto.AuthorId
        };
        try
        {
            var conn = context.Connection();
            var query = "insert into books(title,published_year,genre,author_id) values(@title,@p_y,@genre,@a_id)";
            var res = await conn.ExecuteAsync(query,new{title = book.Title,p_y=book.PublishedYear,genre = book.Genre,a_id = book.AuthorId});
            if (res == 0)
            {
                _logger.LogWarning("In the process of adding book something went wrong");
                return new Response<string>(HttpStatusCode.InternalServerError, "Book not added");
            }
            else
            {
                _logger.LogInformation("While adding book nothing went wrong");
                return new Response<string>(HttpStatusCode.OK, "Book was added successfully");
            }
        }
        catch(Exception ex)
        {
            _logger.LogWarning(ex.Message);
            return new Response<string>(HttpStatusCode.InternalServerError, "Internal Server Error");
        }
    }

    public async Task<Response<string>> DeleteAsync(int bookId)
    {
        _logger.LogInformation("Starting the process of deleting book...");
        try
        {
            var conn = context.Connection();
            var query = "delete from books where id = @id";
            var res = await conn.ExecuteAsync(query,new{id = bookId});
            if (res == 0)
            {
                _logger.LogWarning("In the process of deleting book something went wrong");
                return new Response<string>(HttpStatusCode.InternalServerError, "Book not deleted");
            }
            else
            {
                _logger.LogInformation("While deleting book nothing went wrong");
                return new Response<string>(HttpStatusCode.OK, "Book was deleted successfully");
            }
        }
        catch(Exception ex)
        {
            _logger.LogWarning(ex.Message);
            return new Response<string>(HttpStatusCode.InternalServerError, "Internal Server Error");
        }
    }

    public async Task<Response<Book>> GetBookByIdAsync(int bookId)
    {
        _logger.LogInformation("In the process of getting book...");
        var conn = context.Connection();
        var query = "select * from books where id = @id";
        var res = await conn.QueryFirstOrDefaultAsync(query,new{id = bookId});
        return new Response<Book>(HttpStatusCode.OK, "The data: ", res);
    }

    public async Task<List<Book>> GetBooksAsync()
    {
        _logger.LogInformation("In the process of getting books...");
        var conn = context.Connection();
        var query = "select * from books";
        var res = await conn.QueryAsync<Book>(query);
        return res.ToList();
    }

    public async Task<Response<string>> UpdateAsync(BookUpdateDto bookUpdateDto)
    {
        _logger.LogInformation("In the process of updating book...");
        var book = new Book()
        {
            Title = bookUpdateDto.Title,
            PublishedYear = bookUpdateDto.PublishedYear,
            Genre = bookUpdateDto.Genre,
            AuthorId = bookUpdateDto.AuthorId,
            Id = bookUpdateDto.Id
        };
        try
        {
            var conn = context.Connection();
            var query = "update books set title = @title,published_year = @p_y,genre = @genre,author_id = @a_id where id = @id";
            var res = await conn.ExecuteAsync(query,new{title = book.Title,p_y = book.PublishedYear,genre = book.Genre,id = book.Id});
            if(res == 0)
            {
                _logger.LogWarning("Something went wrong while updating book.");
                return new Response<string>(HttpStatusCode.InternalServerError, "Book not updated");
            }
            else
            {
                _logger.LogInformation("Nothing went wrong while updating this book");
                return new Response<string>(HttpStatusCode.OK, "Book was updated successfully");
            }
        }
        catch(Exception ex)
        {
            _logger.LogWarning(ex.Message);
            return new Response<string>(HttpStatusCode.InternalServerError, "Internal Server Error");
        }    
    }
}
