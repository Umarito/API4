using System.ComponentModel.DataAnnotations;

public class UserDto
{
    [Required(ErrorMessage = "Xay odama mondi nomwam bte")]
    public string? FullName{get;set;}
    public string? Email{get;set;}
    public DateTime RegisteredAt{get;set;}=DateTime.Now;
}
public class UserUpdateDto
{
    [Required(ErrorMessage = "Xay odama mondi nomwam bte")]
    public int Id{get;set;}
    public string? FullName{get;set;}
    public string? Email{get;set;}
}