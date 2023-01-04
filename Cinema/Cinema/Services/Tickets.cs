using Cinema.Data;
using Cinema.Models;
using Cinema.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;

namespace Cinema.Services;

public sealed class Tickets : UniversalCommonsService<Ticket>, ITickets
{
    public Tickets(ILogger<Tickets> logger)
        : base(logger)
    { }

    public async Task UpdateAsync(Ticket ticket)
    {
        await UpdateAsync(ticket.Id, ticket);
    }

    public async Task ArchiveAsync(Guid id)
    {
        await UpdateAsync(id, ticket => ticket.Archived = true);
    }
}