using System.ComponentModel.DataAnnotations.Schema;

namespace Cinema.Models;

public record Order
{
    public Order()
        : this(Array.Empty<Ticket>())
    { }

    public Order(ICollection<Ticket> tickets)
    {
        Tickets = tickets;
        FinalPrice = tickets.Sum(t => t.Price);
    }

    public int Id { get; set; }

    public Client Client { get; set; } = null!;

    public ICollection<Ticket> Tickets { get; set; }

    public DateTime PlacedTime { get; set; }

    [Column(TypeName = "decimal(6, 2)")]
    public decimal FinalPrice { get; set; }
}