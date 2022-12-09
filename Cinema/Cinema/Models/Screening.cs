using Cinema.Models.Interfaces;
using Cinema.Utils;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Cinema.Models;

public sealed record Screening : IEntity<Screening>
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = Generate.BsonId();
    public Movie? Movie { get; set; }
    public DateTime Time { get; set; }
}