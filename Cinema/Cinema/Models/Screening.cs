using Cinema.Models.Interfaces;
using Cinema.Utils;

namespace Cinema.Models;

public sealed record Screening : IEntity
{
    public Guid Id { get; set; } = Generate.Id();
    public Movie? Movie { get; set; }
    public DateTime Time { get; set; }
}