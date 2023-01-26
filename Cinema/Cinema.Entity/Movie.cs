using Cinema.Entity.Enums;
using Cinema.Entity.Interfaces;
using Cinema.Entity.Utils;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Cinema.Entity;

public sealed record Movie : IMongoEntity<Movie>
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = Generate.Id();
    public string? Title { get; set; }
    public int Length { get; set; }
    public AgeCategory AgeCategory { get; set; }
}