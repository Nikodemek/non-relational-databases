namespace Cinema.Models;

public record Screening
{
    public int Id { get; set; }
    
    public int MovieId { get; set; }
    
    public DateTime Time { get; set; }
    
    
    public virtual Movie Movie { get; set; } = null!;
}