using Cinema.Mappers.Interfaces;
using Cinema.Models;
using Cinema.Models.Dto;
using Cinema.Services.Interfaces;

namespace Cinema.Mappers;

public class TicketMapper : IEntityMapper<Ticket, TicketDto>
{
    private readonly IScreenings _screenings;

    public TicketMapper(IScreenings screenings)
    {
        _screenings = screenings;
    }

    public async Task<Ticket> ToEntity(TicketDto dto)
    {
        return new Ticket()
        {
            Id = dto.Id,
            Price = dto.Price,
            Screening = await _screenings.GetAsync(dto.ScreeningId),
            Sold = dto.Sold,
            Archived = dto.Archived,
        };
    }

    public Task<TicketDto> ToDto(Ticket entity)
    {
        return Task.FromResult(new TicketDto()
        {
            Id = entity.Id,
            Price = entity.Price,
            ScreeningId = entity.Screening?.Id ?? Guid.Empty,
            Sold = entity.Sold,
            Archived = entity.Archived,
        });
    }
}