using System.ComponentModel.DataAnnotations.Schema;
using Cinema.Models.Interfaces;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Cinema.Models;

public sealed record Client : IMongoEntity<Client>
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public ObjectId Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime Birthday { get; set; }
    public ClientType ClientType { get; set; }
    public Address? Address { get; set; }
    public decimal AccountBalance { get; set; }
    public bool Archived { get; set; }
    
}