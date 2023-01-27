using System.Collections.Generic;
using System.Threading.Tasks;
using Cinema.Entity;
using Cinema.Repository.Interfaces;
using Cinema.Services.Interfaces;
using MongoDB.Driver;

namespace Cinema.Services;

public sealed class TicketService : CommonService<Ticket>, ITicketService
{
    private readonly ITicketsRepository _ticketsRepository;

    public TicketService(ITicketsRepository ticketsRepository)
        : base(ticketsRepository)
    {
        _ticketsRepository = ticketsRepository;
    }
    
    public Task<IAsyncCursor<Ticket>> GetWithIdsAsync(ICollection<string> ids)
    {
        return _ticketsRepository.GetWithIdsAsync(ids);
    }

    public Task<ReplaceOneResult> UpdateAsync(string id, Ticket ticket)
    {
        return _ticketsRepository.UpdateAsync(id, ticket);
    }

    public Task<ReplaceOneResult> ArchiveAsync(string id)
    {
        return _ticketsRepository.ArchiveAsync(id);
    }
}