using MongoDB.Bson;

namespace Cinema.Models.Interfaces;

public interface IEntity
{
    string Id { get; set; }
}