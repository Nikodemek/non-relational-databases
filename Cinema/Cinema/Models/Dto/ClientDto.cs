using Cinema.Models.Dto.Interfaces;
using Cinema.Utils;

namespace Cinema.Models.Dto;

public sealed record ClientDto : IEntityDto
{
    public Guid Id { get; set; } = Generate.Id();
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime Birthday { get; set; }
    public int ClientType { get; set; }
    public Guid AddressId { get; set; }
    public decimal AccountBalance { get; set; }
    public bool Archived { get; set; }
}