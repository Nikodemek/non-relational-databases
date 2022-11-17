using Cinema.Models.Interfaces;

namespace Cinema.Models;

public record Address : IMongoEntity<Address>
{
    public int Id { get; set; }
    
    public string? Country { get; set; }
    
    public string? City { get; set; }
    
    public string? Street { get; set; }
    
    public string? Number { get; set; }
}