using Cinema.Models.Interfaces;
using Cinema.Utils;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Cinema.Models;

public sealed record Ticket : IEntity<Ticket>
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = Generate.BsonId();
    public decimal Price { get; set; }
    public Screening? Screening { get; set; }
    public bool Sold { get; set; }
    public bool Archived { get; set; }
}