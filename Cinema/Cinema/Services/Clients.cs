using Cassandra;
using Cinema.Mappers.Interfaces;
using Cinema.Models;
using Cinema.Models.Dto;
using Cinema.Services.Interfaces;

namespace Cinema.Services;

public sealed class Clients : UniversalCommonsService<Client, ClientDto>, IClients
{
    public Clients(ILogger<Clients> logger, IEntityMapper<Client, ClientDto> mapper)
        : base(logger, mapper)
    { }

    public async Task<RowSet> ArchiveAsync(Guid id)
    {
        return await UpdateAsync(id, client => client.Archived = true);
    }
}