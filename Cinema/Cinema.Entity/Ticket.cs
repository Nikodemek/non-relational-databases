using Cinema.Entity.Interfaces;
using Cinema.Entity.Utils;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Cinema.Entity;

public sealed record Ticket : IMongoEntity<Ticket>
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = Generate.Id();
    public decimal Price { get; set; }
    public Screening? Screening { get; set; }
    public bool Sold { get; set; }
    public bool Archived { get; set; }
}