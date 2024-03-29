﻿using Cinema.Entity;
using MongoDB.Driver;

namespace Cinema.Repository.Interfaces;

public interface IClientsRepository : ICommonsRepository<Client>
{
    Task<ReplaceOneResult> UpdateAsync(string id, Client client);
    Task<ReplaceOneResult> ArchiveAsync(string id);
}