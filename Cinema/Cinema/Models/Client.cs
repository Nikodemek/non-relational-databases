using Cinema.Models.Interfaces;
using Cinema.Utils;

namespace Cinema.Models;

public sealed record Client : IEntity
{
    public Guid Id { get; set; } = Generate.Id();
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime Birthday { get; set; }
    public ClientType ClientType { get; set; }
    public Address? Address { get; set; }
    public decimal AccountBalance { get; set; }
    public bool Archived { get; set; }
}