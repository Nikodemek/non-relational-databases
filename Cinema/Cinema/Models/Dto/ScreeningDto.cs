using Cinema.Models.Dto.Interfaces;
using Cinema.Utils;

namespace Cinema.Models.Dto;

public sealed record ScreeningDto : IEntityDto
{
    public Guid Id { get; set; } = Generate.Id();
    public Guid MovieId { get; set; }
    public DateTime Time { get; set; }
}