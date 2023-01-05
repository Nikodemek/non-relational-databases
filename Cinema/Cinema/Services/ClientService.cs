using Cassandra;
using Cinema.Mappers.Interfaces;
using Cinema.Models;
using Cinema.Models.Dto;
using Cinema.Services.Interfaces;

namespace Cinema.Services;

public sealed class ClientService : CommonService<Client, ClientDto>, IClientService
{
    public ClientService(ILogger<ClientService> logger, IEntityMapper<Client, ClientDto> mapper)
        : base(logger, mapper)
    { }

    public async Task<RowSet> ArchiveAsync(Guid id)
    {
        return await UpdateAsync(id, client => client.Archived = true);
    }
}