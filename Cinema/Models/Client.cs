using System.ComponentModel.DataAnnotations.Schema;

namespace Cinema.Models;

public record Client
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public DateTime Birthday { get; set; }

    public int? AddressId { get; set; } = null!;

    public string? PhoneNumber { get; set; }

    public ClientType ClientType { get; set; }
    
    [Column(TypeName = "decimal(6, 2)")]
    public decimal AccountBalance { get; set; }

    public bool Archived { get; set; }
    
    
    public virtual Address? Address { get; set; } = null!;
}