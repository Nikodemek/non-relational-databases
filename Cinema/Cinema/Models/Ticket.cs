using Cinema.Models.Interfaces;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Cinema.Models;

public sealed record Ticket : IMongoEntity<Ticket>
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public ObjectId Id { get; set; }
    public decimal Price { get; set; }
    public Screening? Screening { get; set; }
    public bool Sold { get; set; }
    public bool Archived { get; set; }
}