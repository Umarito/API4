public interface IBookLoanService
{
    Task<Response<string>> AddAsync(BookLoan bookLoan);
    Task<List<BookLoan>> GetBookLoansAsync();
    Task<Response<UserWithLoans>> GetBookLoanJoinAsync(int bookLoanId);
    Task<Response<string>> UpdateAsync(BookLoan bookLoan);
    Task<Response<string>> DeleteAsync(int bookLoanId);
    Task<Response<string>> ReturnBook(int bookLoanId);
}