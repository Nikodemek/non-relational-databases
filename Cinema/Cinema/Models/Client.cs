using Cinema.Models.Interfaces;
using Cinema.Utils;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Cinema.Models;

public sealed record Client() : IEntity
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = Generate.BsonId();
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime Birthday { get; set; }
    public ClientType ClientType { get; set; }
    public Address? Address { get; set; }
    public decimal AccountBalance { get; set; }
    public bool Archived { get; set; }
}