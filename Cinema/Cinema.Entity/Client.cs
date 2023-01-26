using Cinema.Entity.Enums;
using Cinema.Entity.Interfaces;
using Cinema.Entity.Utils;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Cinema.Entity;

public sealed record Client : IMongoEntity<Client>
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = Generate.Id();
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime Birthday { get; set; }
    public ClientType ClientType { get; set; }
    public Address? Address { get; set; }
    public decimal AccountBalance { get; set; }
    public bool Archived { get; set; }
    
}