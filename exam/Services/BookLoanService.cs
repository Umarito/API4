using System.Net;
using Dapper;
using Microsoft.AspNetCore.Authorization;

public class BookLoanService(ApplicationDBContext dBContext,ILogger<BookLoanService> logger) : IBookLoanService
{
    private readonly ILogger<BookLoanService> _logger = logger;
    private readonly ApplicationDBContext context = dBContext;
    public async Task<Response<string>> AddAsync(BookLoan bookLoan)
    {
        _logger.LogInformation("Starting the process of adding book loan...");
        try
        {
            var conn = context.Connection();
            var query = "insert into book_loans(book_id,user_id) values(@b_id,@u_id)";
            var res = await conn.ExecuteAsync(query,new{b_id = bookLoan.BookId,u_id = bookLoan.UserId});
            if(res == 0)
            {
                _logger.LogWarning("In the process of adding book loan something went wrong");
                return new Response<string>(HttpStatusCode.InternalServerError, "Book Loan not added");
            }
            else
            {
                _logger.LogInformation("While adding book nothing went wrong");
                return new Response<string>(HttpStatusCode.OK, "Book Loan was added successfully");
            }
        }
        catch(Exception ex)
        {
            _logger.LogWarning(ex.Message);
            return new Response<string>(HttpStatusCode.InternalServerError, "Internal Server Error");
        }
    }

    public async Task<Response<string>> DeleteAsync(int bookLoanId)
    {
        _logger.LogInformation("Starting the process of deleting book loan...");
        try
        {
            var conn = context.Connection();
            var query = "delete from book_loans where id = @id";
            var res = await conn.ExecuteAsync(query,new{id = bookLoanId});
            if(res == 0)
            {
                _logger.LogWarning("In the process of deleting book loan something went wrong");                return new Response<string>(HttpStatusCode.InternalServerError, "Book Loan not deleted");
            }
            else
            {
                _logger.LogInformation("While deleting book nothing went wrong");
                return new Response<string>(HttpStatusCode.OK, "Book Loan was deleted successfully");
            }
        }
        catch(Exception ex)
        {
            _logger.LogWarning(ex.Message);
            return new Response<string>(HttpStatusCode.InternalServerError, "Internal Server Error");
        }
    }

    public async Task<Response<UserWithLoans>> GetBookLoanJoinAsync(int bookLoanId)
    {
        _logger.LogInformation("In the process of getting book loans with join...");
        var conn = context.Connection();
        var query = "select bl.user_id,u.fullname,u.email,nl.book_id,b.title,b.genre from book_loans bl join users u on bl.user_id = u.id join books b on bl.book_id = b.id where bl.id = @id";
        var res = await conn.QueryFirstOrDefaultAsync<UserWithLoans>(query,new{id = bookLoanId});
        return new Response<UserWithLoans>(HttpStatusCode.OK, "The data: ", res);
    }

    public async Task<List<BookLoan>> GetBookLoansAsync()
    {
        _logger.LogInformation("In the process of getting book loans...");
        var conn = context.Connection();
        var query = "select * from book_loans";
        var res = await conn.QueryAsync<BookLoan>(query);
        return res.ToList();
    }

    public async Task<Response<string>> UpdateAsync(BookLoan bookLoan)
    {
        _logger.LogInformation("In the process of returning book...");
        try
        {
            var conn = context.Connection();
            var query = "update book_loans set book_id = @b_id,user_id = @u_id where id = @id";
            var res = await conn.ExecuteAsync(query,new{b_id = bookLoan.BookId,u_id = bookLoan.UserId,id = bookLoan.Id});
            if(res == 0)
            {
                _logger.LogWarning("Something went wrong while updating book loan");
                return new Response<string>(HttpStatusCode.InternalServerError, "Book Loan not updated");
            }
            else
            {
                _logger.LogInformation("Nothing went wrong while updating this book");
                return new Response<string>(HttpStatusCode.OK, "Book Loan was updated successfully");
            }
        }
        catch(Exception ex)
        {
            _logger.LogWarning(ex.Message);
            return new Response<string>(HttpStatusCode.InternalServerError, "Internal Server Error");
        }    
    }
    public async Task<Response<string>> ReturnBook(int bookLoanId)
    {
        _logger.LogInformation("In the process of returning book...");
        try
        {
            var conn = context.Connection();
            var query = "update book_loans set return_date = NOW() where id = @id";
            var res = await conn.ExecuteAsync(query,new{id = bookLoanId});
            if(res == 0)
            {
                _logger.LogWarning("Something went wrong while returning book.");
                return new Response<string>(HttpStatusCode.InternalServerError, "Book wasn't returned");
            }
            else
            {
                _logger.LogInformation("Nothing went wrong while returning this book");
                return new Response<string>(HttpStatusCode.OK, "Book returned successfully");
            }
        }
        catch(Exception ex)
        {
            _logger.LogWarning(ex.Message);
            return new Response<string>(HttpStatusCode.InternalServerError, "Internal Server Error");
        }
    }
}
