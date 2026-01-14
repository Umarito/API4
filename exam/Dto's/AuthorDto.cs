using System.ComponentModel.DataAnnotations;

public class AuthorDto
{
    [Required(ErrorMessage = "Yagonbor diday avtor be nom bowa? Nomwa pr k")]
    public string FullName{get;set;}=null!;
    [Required(ErrorMessage = "Avtort kli be dokumentay? Xay soli tavaludwam bte oxi")]
    public DateTime? BirthDate{get;set;}
    [Required(ErrorMessage = "Brat avtort besoibay? Soib dora?")]
    public string? Country{get;set;}
}
public class AuthorUpdateDto
{
    public int Id{get;set;}
    [Required(ErrorMessage = "Yagonbor diday avtor be nom bowa? Nomwa pr k")]
    public string FullName{get;set;}=null!;
    [Required(ErrorMessage = "Avtort kli be dokumentay? Xay soli tavaludwam bte oxi")]
    public DateTime? BirthDate{get;set;}
    [Required(ErrorMessage = "Brat avtort besoibay? Soib dora?")]
    public string? Country{get;set;}
}