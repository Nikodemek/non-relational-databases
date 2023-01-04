using Cinema.Models.Interfaces;
using Cinema.Utils;

namespace Cinema.Models;

public sealed record Order : IEntity
{
    public Order()
        : this(new HashSet<Ticket>())
    { }

    public Order(ICollection<Ticket> tickets)
    {
        Tickets = tickets;
        FinalPrice = tickets.Sum(t => t.Price);
    }
    
    public Guid Id { get; set; } = Generate.Id();
    public Client? Client { get; set; }
    public DateTime PlacedTime { get; set; }
    public decimal FinalPrice { get; set; }
    public bool Success { get; set; }
    public ICollection<Ticket> Tickets { get; set; }
}