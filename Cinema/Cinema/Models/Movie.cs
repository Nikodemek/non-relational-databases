namespace Cinema.Models;

public record Movie
{
    public int Id { get; set; }
    
    public string Title { get; set; } = null!;
    
    public int Length { get; set; }
    
    public AgeCategory AgeCategory { get; set; }
}