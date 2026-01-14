using System.ComponentModel.DataAnnotations;

public class Author:BaseEntity
{
    public string FullName{get;set;}=null!;
    public DateTime? BirthDate{get;set;}
    public string? Country{get;set;}
}