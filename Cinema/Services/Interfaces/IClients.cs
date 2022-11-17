using Cinema.Models;
using MongoDB.Driver;

namespace Cinema.Services.Interfaces;

public interface IClients : ICommonService<Client>
{
    Task<ReplaceOneResult> UpdateAsync(Client client);
    Task<ReplaceOneResult> ArchiveAsync(int id);
    Task RemoveAsync(int id);
}