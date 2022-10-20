using System.ComponentModel.DataAnnotations.Schema;

namespace Cinema.Models;

public record Ticket
{
    public int Id { get; set; }
    
    public Screening Screening { get; set; } = null!;
    
    [Column(TypeName = "decimal(6, 2)")]
    public decimal Price { get; set; }
    
    public bool Sold { get; set; }
    
    public bool Archived { get; set; }
}