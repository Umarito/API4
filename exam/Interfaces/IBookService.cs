public interface IBookService
{
    Task<Response<string>> AddAsync(BookDto bookDto);
    Task<List<Book>> GetBooksAsync();
    Task<Response<Book>> GetBookByIdAsync(int bookId);
    Task<Response<string>> UpdateAsync(BookUpdateDto book);
    Task<Response<string>> DeleteAsync(int bookId);
}