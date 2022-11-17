using Cinema.Models.Interfaces;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Cinema.Models;

public sealed record Address : IMongoEntity<Address>
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public ObjectId Id { get; set; }
    public string? Country { get; set; }
    
    public string? City { get; set; }
    
    public string? Street { get; set; }
    
    public string? Number { get; set; }
}