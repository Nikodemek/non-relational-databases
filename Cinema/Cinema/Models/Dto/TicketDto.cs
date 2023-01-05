using Cinema.Models.Dto.Interfaces;
using Cinema.Utils;

namespace Cinema.Models.Dto;

public sealed record TicketDto : IEntityDto
{
    public Guid Id { get; set; } = Generate.Id();
    public decimal Price { get; set; }
    public Guid ScreeningId { get; set; }
    public bool Sold { get; set; }
    public bool Archived { get; set; }
}