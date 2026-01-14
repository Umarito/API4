using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class AuthorsController(IAuthorService authorService) : ControllerBase
{
    [HttpPost]
    public async Task<Response<string>> AddAsync(AuthorDto author)
    {
        return await authorService.AddAsync(author);
    }
    [HttpPut]
    public async Task<Response<string>> UpdateAsync(AuthorUpdateDto author)
    {
        return await authorService.UpdateAsync(author);
    }
    [HttpDelete("{authorId}")]
    public async Task<Response<string>> DeleteAsync(int authorId)
    {
        return await authorService.DeleteAsync(authorId);
    }
    [HttpGet]
    public async Task<List<Author>> GetAuthorsAsync()
    {
        return await authorService.GetAuthorsAsync();
    }
    
    [HttpGet("{authorId}")]
    public async Task<Response<Author>> GetAuthorByIdAsync(int authorId)
    {
        return await authorService.GetAuthorByIdAsync(authorId);
    }
}