using System.ComponentModel.DataAnnotations;

public class User:BaseEntity
{
    public string? FullName{get;set;}
    public string? Email{get;set;}
    public DateTime RegisteredAt{get;set;}=DateTime.Now;
}