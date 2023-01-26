using Cinema.Entity;
using MongoDB.Driver;

namespace Cinema.Services.Interfaces;

public interface IClientService : ICommonService<Client>
{
    Task<ReplaceOneResult> UpdateAsync(string id, Client client);
    Task<ReplaceOneResult> ArchiveAsync(string id);
}