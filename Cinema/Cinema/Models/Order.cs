using System.ComponentModel.DataAnnotations.Schema;
using Cinema.Models.Interfaces;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Cinema.Models;

public sealed record Order : IMongoEntity<Order>
{
    public Order()
        : this(new HashSet<Ticket>())
    { }

    public Order(ICollection<Ticket> tickets)
    {
        Tickets = tickets;
        FinalPrice = tickets.Sum(t => t.Price);
    }
    
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public ObjectId Id { get; set; }
    public Client? Client { get; set; }
    public DateTime PlacedTime { get; set; }
    public decimal FinalPrice { get; set; }
    public bool Success { get; set; }
    public ICollection<Ticket> Tickets { get; set; }
}