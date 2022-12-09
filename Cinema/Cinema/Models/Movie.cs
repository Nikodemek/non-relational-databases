using Cinema.Models.Interfaces;
using Cinema.Utils;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Cinema.Models;

public sealed record Movie : IEntity<Movie>
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = Generate.BsonId();
    public string? Title { get; set; }
    public int Length { get; set; }
    public AgeCategory AgeCategory { get; set; }
}