using Cinema.Models.Interfaces;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Cinema.Models;

public record Movie : IMongoEntity<Movie>
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public int Id { get; set; }
    public string Title { get; set; }
    public int Length { get; set; }
    public AgeCategory AgeCategory { get; set; }
}