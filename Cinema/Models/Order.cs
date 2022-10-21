using System.ComponentModel.DataAnnotations.Schema;

namespace Cinema.Models;

public record Order
{
    public Order()
        : this(new HashSet<Ticket>())
    { }

    public Order(ICollection<Ticket> tickets)
    {
        Tickets = tickets;
        FinalPrice = tickets.Sum(t => t.Price);
    }

    public int Id { get; set; }

    public int ClientId { get; set; }

    public DateTime PlacedTime { get; set; } = DateTime.Now;

    [Column(TypeName = "decimal(6, 2)")]
    public decimal FinalPrice { get; set; }

    public bool Success { get; set; }
    

    public virtual Client Client { get; set; } = null!;
    
    public virtual ICollection<Ticket> Tickets { get; set; }
}