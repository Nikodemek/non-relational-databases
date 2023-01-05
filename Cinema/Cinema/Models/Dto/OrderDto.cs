using Cinema.Models.Dto.Interfaces;
using Cinema.Utils;

namespace Cinema.Models.Dto;

public sealed record OrderDto : IEntityDto
{
    public Guid Id { get; set; } = Generate.Id();
    public Guid ClientId { get; set; }
    public DateTime PlacedTime { get; set; }
    public decimal FinalPrice { get; set; }
    public bool Success { get; set; }
    public IEnumerable<Guid> TicketIds { get; set; } = Array.Empty<Guid>();
}