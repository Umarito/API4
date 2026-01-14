using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class BookLoansController(IBookLoanService bookLoanService) : ControllerBase
{
    [HttpPost]
    public async Task<Response<string>> AddAsync(BookLoan bookLoan)
    {
        return await bookLoanService.AddAsync(bookLoan);
    }
    [HttpPut]
    public async Task<Response<string>> UpdateAsync(BookLoan bookLoan)
    {
        return await bookLoanService.UpdateAsync(bookLoan);
    }
    [HttpDelete]
    public async Task<Response<string>> DeleteAsync(int bookLoanId)
    {
        return await bookLoanService.DeleteAsync(bookLoanId);
    }
    [HttpGet]
    public async Task<List<BookLoan>> GetBookLoansAsync()
    {
        return await bookLoanService.GetBookLoansAsync();
    }
    
    [HttpGet("{bookLoanId}")]
    public async Task<Response<UserWithLoans>> GetBookLoanJoinAsync(int bookLoanId)
    {
        return await bookLoanService.GetBookLoanJoinAsync(bookLoanId);
    }
    [HttpPut("{bookId}")]
    public async Task<Response<string>> ReturnBook(int bookId)
    {
        return await bookLoanService.ReturnBook(bookId);
    }
}