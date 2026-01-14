public class BookLoan : BaseEntity
{
    public int BookId{get;set;}
    public int UserId{get;set;}
    public DateTime LoanDate{get;set;}=DateTime.Now;
    public DateTime ReturnDate{get;set;}
}