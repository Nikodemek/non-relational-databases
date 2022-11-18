using MongoDB.Bson;

namespace Cinema.Models.Interfaces;

public interface IMongoEntity<T>
{
    string Id { get; set; }
}