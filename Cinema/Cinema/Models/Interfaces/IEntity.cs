using MongoDB.Bson;

namespace Cinema.Models.Interfaces;

public interface IEntity<T>
{
    string Id { get; set; }
}