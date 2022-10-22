using System.ComponentModel.DataAnnotations.Schema;

namespace Cinema.Models;

public record Ticket
{
    public int Id { get; set; }
    
    public int ScreeningId { get; set; }
    
    [Column(TypeName = "decimal(6, 2)")]
    public decimal Price { get; set; }
    
    public bool Sold { get; set; }
    
    public bool Archived { get; set; }
    
    
    public virtual Screening Screening { get; set; } = null!;
}