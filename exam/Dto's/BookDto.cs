using System.ComponentModel.DataAnnotations;

public class BookDto
{
    [Required(ErrorMessage = "Brachka kitobta nomwa baba xurdak cho kad?")]
    public string? Title{get;set;}
    public int PublishedYear{get;set;}
    public string? Genre{get;set;}
    public int AuthorId{get;set;}
}
public class BookUpdateDto
{
    public int Id{get;set;}
    [Required(ErrorMessage = "Brachka kitobta nomwa baba xurdak cho kad?")]
    public string? Title{get;set;}
    public int PublishedYear{get;set;}
    public string? Genre{get;set;}
    public int AuthorId{get;set;}
}