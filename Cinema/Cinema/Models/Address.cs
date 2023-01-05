using Cinema.Models.Interfaces;
using Cinema.Utils;

namespace Cinema.Models;

public sealed record Address : IEntity
{
    public Guid Id { get; set; } = Generate.Id();
    public string? Country { get; set; }
    public string? City { get; set; }
    public string? Street { get; set; }
    public string? Number { get; set; }
}