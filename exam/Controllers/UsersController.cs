using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class UsersController(IUserService userService) : ControllerBase
{
    [HttpPost]
    public async Task<Response<string>> AddAsync(UserDto user)
    {
        return await userService.AddAsync(user);
    }
    [HttpPut]
    public async Task<Response<string>> UpdateAsync(UserUpdateDto user)
    {
        return await userService.UpdateAsync(user);
    }
    [HttpDelete]
    public async Task<Response<string>> DeleteAsync(int userId)
    {
        return await userService.DeleteAsync(userId);
    }
    [HttpGet]
    public async Task<List<User>> GetUsersAsync()
    {
        return await userService.GetUsersAsync();
    }
    
    [HttpGet("{userId}")]
    public async Task<Response<User>> GetUserByIdAsync(int userId)
    {
        return await userService.GetUserByIdAsync(userId);
    }
}