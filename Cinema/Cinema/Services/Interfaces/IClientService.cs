using Cassandra;
using Cinema.Models;

namespace Cinema.Services.Interfaces;

public interface IClientService : ICommonService<Client>
{
    Task<RowSet> ArchiveAsync(Guid id);
}