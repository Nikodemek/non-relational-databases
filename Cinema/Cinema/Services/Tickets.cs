using Cinema.Data;
using Cinema.Mappers.Interfaces;
using Cinema.Models;
using Cinema.Models.Dto;
using Cinema.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;

namespace Cinema.Services;

public sealed class Tickets : UniversalCommonsService<Ticket, TicketDto>, ITickets
{
    public Tickets(ILogger<Tickets> logger, IEntityMapper<Ticket, TicketDto> mapper)
        : base(logger, mapper)
    { }

    public async Task ArchiveAsync(Guid id)
    {
        await UpdateAsync(id, ticket => ticket.Archived = true);
    }
}