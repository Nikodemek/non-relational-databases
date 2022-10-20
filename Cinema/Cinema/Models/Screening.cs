namespace Cinema.Models;

public record Screening
{
    public int Id { get; set; }
    
    public Movie Movie { get; set; } = null!;
    
    public DateTime Time { get; set; }
}