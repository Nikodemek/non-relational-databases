using Cassandra;
using Cinema.Data;
using Cinema.Mappers.Interfaces;
using Cinema.Models;
using Cinema.Models.Dto;
using Cinema.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;

namespace Cinema.Services;

public sealed class TicketService : CommonService<Ticket, TicketDto>, ITicketService
{
    public TicketService(ILogger<TicketService> logger, IEntityMapper<Ticket, TicketDto> mapper)
        : base(logger, mapper)
    { }

    public async Task<RowSet> ArchiveAsync(Guid id)
    {
        return await UpdateAsync(id, ticket => ticket.Archived = true);
    }
}