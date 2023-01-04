using Cinema.Models;
using Cinema.Services.Interfaces;

namespace Cinema.Services;

public sealed class Clients : UniversalCommonsService<Client>, IClients
{
    public Clients(ILogger<Clients> logger)
        : base(logger)
    { }
    
    public async Task UpdateAsync(Client client)
    {
        await UpdateAsync(client.Id, client);
    }

    public async Task ArchiveAsync(Guid id)
    {
        await UpdateAsync(id, client => client.Archived = true);
    }
}