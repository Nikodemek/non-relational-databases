using Cinema.Models.Dto.Interfaces;
using Cinema.Utils;

namespace Cinema.Models.Dto;

public sealed record AddressDto : IEntityDto
{
    public Guid Id { get; set; } = Generate.Id();
    public string? Country { get; set; }
    public string? City { get; set; }
    public string? Street { get; set; }
    public string? Number { get; set; }
}