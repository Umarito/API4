using System.Net;
using Dapper;
using Microsoft.AspNetCore.Authorization;

public class AuthorService(ApplicationDBContext dBContext,ILogger<AuthorService> logger) : IAuthorService
{
    private readonly ILogger<AuthorService> _logger = logger;
    private readonly ApplicationDBContext context = dBContext;
    public async Task<Response<string>> AddAsync(AuthorDto authorDto)
    {
        _logger.LogInformation("Starting the process of adding author");
        var author = new Author()
        {
            FullName = authorDto.FullName,
            BirthDate = authorDto.BirthDate,
            Country = authorDto.Country
        };
        try
        {
            var conn = context.Connection();
            var query = "insert into authors(fullname,birth_date,country) values(@name,@b_date,@c)";
            var res = await conn.ExecuteAsync(query,new{name = author.FullName,b_date = author.BirthDate,c = author.Country});
            if(res == 0)
            {
                _logger.LogWarning("In the process something went wrong");
                return new Response<string>(HttpStatusCode.InternalServerError, "Author not added");
            }
            else
            {
                _logger.LogInformation("In the process nothing went wrong");
                return new Response<string>(HttpStatusCode.OK, "Author was added successfully");
            }
        }
        catch(Exception ex)
        {
            _logger.LogWarning(ex.Message);
            return new Response<string>(HttpStatusCode.InternalServerError, "Internal Server Error");
        }
    }

    public async Task<Response<string>> DeleteAsync(int authorId)
    {
        _logger.LogInformation("Starting the process of deleting...");
        try
        {
            var conn = context.Connection();
            var query = "delete from authors where id = @id";
            var res = await conn.ExecuteAsync(query,new{id = authorId});
            if(res == 0)
            {
                _logger.LogWarning("Something went wrong in the process of deleting author");
                return new Response<string>(HttpStatusCode.InternalServerError, "Author not deleted");
            }
            else
            {
                _logger.LogInformation("In the process of deleting author nothing went wrong");
                return new Response<string>(HttpStatusCode.OK, "Author was deleted successfully");
            }
        }
        catch(Exception ex)
        {
            _logger.LogWarning(ex.Message);
            return new Response<string>(HttpStatusCode.InternalServerError, "Internal Server Error");
        }
    }

    public async Task<Response<Author>> GetAuthorByIdAsync(int authorId)
    {
        _logger.LogInformation("Searching author by id is processing...");
        var conn = context.Connection();
        var query = "select * from authors where id = @id";
        var res = await conn.QueryFirstOrDefaultAsync(query,new{id = authorId});
        return new Response<Author>(HttpStatusCode.OK, "The data: ", res);
    }

    public async Task<List<Author>> GetAuthorsAsync()
    {
        _logger.LogInformation("Starting the process of getting all authors...");
        var conn = context.Connection();
        var query = "select * from authors";
        var res = await conn.QueryAsync<Author>(query);
        return res.ToList();
    }

    public async Task<Response<string>> UpdateAsync(AuthorUpdateDto authorUpdateDto)
    {
        _logger.LogInformation("The process of updating author started...");
        var author = new Author()
        {
            FullName = authorUpdateDto.FullName,
            BirthDate = authorUpdateDto.BirthDate,
            Country = authorUpdateDto.Country,
            Id = authorUpdateDto.Id
        };
        try
        {
            var conn = context.Connection();
            var query = "update authors set fullname = @name,birth_date = @b_d,country = @c where id = @id";
            var res = await conn.ExecuteAsync(query,new{name = author.FullName,b_d = author.BirthDate,c=author.Country,id = author.Id});
            if(res == 0)
            {   
                _logger.LogWarning("Something went wrong in the process of updating author");
                return new Response<string>(HttpStatusCode.InternalServerError, "Author not updated");
            }
            else
            {
                _logger.LogInformation("In the process of updating author nothing went wrong");
                return new Response<string>(HttpStatusCode.OK, "Author was updated successfully");
            }
        }
        catch(Exception ex)
        {
            _logger.LogWarning(ex.Message);
            return new Response<string>(HttpStatusCode.InternalServerError, "Internal Server Error");
        }    
    }
}
