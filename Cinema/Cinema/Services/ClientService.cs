using Cinema.Entity;
using Cinema.Repository.Interfaces;
using Cinema.Services.Interfaces;
using MongoDB.Driver;

namespace Cinema.Services;

public sealed class ClientService : CommonService<Client>, IClientService
{
    private readonly IClientsRepository _clientsRepository;
    
    public ClientService(IClientsRepository clientsRepository)
        : base(clientsRepository)
    {
        _clientsRepository = clientsRepository;
    }
    
    public Task<ReplaceOneResult> UpdateAsync(string id, Client client)
    {
        return _clientsRepository.UpdateAsync(id, client);
    }

    public Task<ReplaceOneResult> ArchiveAsync(string id)
    {
        return _clientsRepository.ArchiveAsync(id);
    }
}