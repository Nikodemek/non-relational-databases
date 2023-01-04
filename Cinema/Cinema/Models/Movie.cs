using Cinema.Models.Interfaces;
using Cinema.Utils;

namespace Cinema.Models;

public sealed record Movie : IEntity
{
    public Guid Id { get; set; } = Generate.Id();
    public string? Title { get; set; }
    public int Length { get; set; }
    public AgeCategory AgeCategory { get; set; }
}