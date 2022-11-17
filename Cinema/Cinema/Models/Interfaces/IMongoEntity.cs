using MongoDB.Bson;

namespace Cinema.Models.Interfaces;

public interface IMongoEntity<T>
{
    ObjectId Id { get; set; }
}