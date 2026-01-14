using System.Net;
using Dapper;
using Microsoft.AspNetCore.Authorization;

public class UserService(ApplicationDBContext dBContext,ILogger<UserService> logger) : IUserService
{
    private readonly ILogger<UserService> _logger = logger;
    private readonly ApplicationDBContext context = dBContext;
    public async Task<Response<string>> AddAsync(UserDto userDto)
    {
        _logger.LogInformation("Starting the process of adding user...");
        var user = new User()
        {
            FullName = userDto.FullName,
            Email = userDto.Email,
            RegisteredAt = userDto.RegisteredAt
        };
        try
        {
            var conn = context.Connection();
            var query = "insert into users(fullname,email) values(@name,@email)";
            var res = await conn.ExecuteAsync(query,new{name = user.FullName,email = user.Email});
            if(res == 0)
            {
                _logger.LogWarning("Something went wrong in process of adding user");
                return new Response<string>(HttpStatusCode.InternalServerError, "User not added");
            }
            else
            {
                _logger.LogInformation("Nothing went wrong while adding the user");
                return new Response<string>(HttpStatusCode.OK, "User was added successfully");
            }
        }
        catch(Exception ex)
        {
            _logger.LogWarning(ex.Message);
            return new Response<string>(HttpStatusCode.InternalServerError, "Internal Server Error");
        }
    }

    public async Task<Response<string>> DeleteAsync(int userId)
    {
        _logger.LogInformation("Starting the process of deleting user...");
        try
        {
            var conn = context.Connection();
            var query = "delete from users where id = @id";
            var res = await conn.ExecuteAsync(query,new{id = userId});
            if(res == 0)
            {
                _logger.LogWarning("Something went wrong in process of deleting the user");
                return new Response<string>(HttpStatusCode.InternalServerError, "User not deleted");
            }
            else
            {
                _logger.LogInformation("Nothing went wrong while deleting the user");
                return new Response<string>(HttpStatusCode.OK, "User was deleted successfully");
            }
        }
        catch(Exception ex)
        {
            _logger.LogWarning(ex.Message);
            return new Response<string>(HttpStatusCode.InternalServerError, "Internal Server Error");
        }
    }

    public async Task<Response<User>> GetUserByIdAsync(int userId)
    {
        _logger.LogInformation("Starting the process of getting user...");
        var conn = context.Connection();
        var query = "select * from users where id = @id";
        var res = await conn.QueryFirstOrDefaultAsync(query,new{id = userId});
        return new Response<User>(HttpStatusCode.OK, "The data: ", res);
    }

    public async Task<List<User>> GetUsersAsync()
    {
        _logger.LogInformation("Starting the process of getting user...");
        var conn = context.Connection();
        var query = "select * from users";
        var res = await conn.QueryAsync<User>(query);
        return res.ToList();
    }

    public async Task<Response<string>> UpdateAsync(UserUpdateDto userUpdateDto)
    {
        _logger.LogInformation("The process of updating user started...");
        var user = new User()
        {
            FullName = userUpdateDto.FullName,
            Email = userUpdateDto.Email,
            Id = userUpdateDto.Id
        };
        try
        {
            var conn = context.Connection();
            var query = "update users set fullname = @name,email = @email where id = @id";
            var res = await conn.ExecuteAsync(query,new{name = user.FullName,email = user.Email,id = user.Id});
            if(res == 0)
            {
                _logger.LogWarning("Something went wrong in process of updating user");
                return new Response<string>(HttpStatusCode.InternalServerError, "User not updated");
            }
            else
            {
                _logger.LogInformation("Nothing went wrong while updating the user");
                return new Response<string>(HttpStatusCode.OK, "User was updated successfully");
            }
        }
        catch(Exception ex)
        {
            _logger.LogWarning(ex.Message);
            return new Response<string>(HttpStatusCode.InternalServerError, "Internal Server Error");
        }    
    }
}
