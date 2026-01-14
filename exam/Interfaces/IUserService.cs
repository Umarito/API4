public interface IUserService
{
    Task<Response<string>> AddAsync(UserDto userDto);
    Task<List<User>> GetUsersAsync();
    Task<Response<User>> GetUserByIdAsync(int userId);
    Task<Response<string>> UpdateAsync(UserUpdateDto user);
    Task<Response<string>> DeleteAsync(int userId);
}