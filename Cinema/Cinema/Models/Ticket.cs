using Cinema.Models.Interfaces;
using Cinema.Utils;

namespace Cinema.Models;

public sealed record Ticket : IEntity
{
    public Guid Id { get; set; } = Generate.Id();
    public decimal Price { get; set; }
    public Screening? Screening { get; set; }
    public bool Sold { get; set; }
    public bool Archived { get; set; }
}