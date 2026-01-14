public interface IAuthorService
{
    Task<Response<string>> AddAsync(AuthorDto authorDto);
    Task<List<Author>> GetAuthorsAsync();
    Task<Response<Author>> GetAuthorByIdAsync(int authorId);
    Task<Response<string>> UpdateAsync(AuthorUpdateDto author);
    Task<Response<string>> DeleteAsync(int authorId);
}