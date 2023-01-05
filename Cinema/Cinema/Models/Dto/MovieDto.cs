using Cinema.Models.Dto.Interfaces;
using Cinema.Utils;

namespace Cinema.Models.Dto;

public sealed record MovieDto : IEntityDto
{
    public Guid Id { get; set; } = Generate.Id();
    public string? Title { get; set; }
    public int Length { get; set; }
    public int AgeCategory { get; set; }
}