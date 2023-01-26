using Cinema.Entity.Interfaces;
using Cinema.Entity.Utils;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Cinema.Entity;

public sealed record Address : IMongoEntity<Address>
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = Generate.Id();
    public string? Country { get; set; }
    
    public string? City { get; set; }
    
    public string? Street { get; set; }
    
    public string? Number { get; set; }
}