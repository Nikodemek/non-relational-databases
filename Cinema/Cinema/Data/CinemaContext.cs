using Cinema.Models;
using Cinema.Utils;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Data;

public class CinemaContext : DbContext
{
    public DbSet<Address> Addresses { get; set; } = null!;

    public DbSet<Client> Clients { get; set; } = null!;

    public DbSet<Movie> Movies { get; set; } = null!;

    public DbSet<Screening> Screenings { get; set; } = null!;

    public DbSet<Ticket> Tickets { get; set; } = null!;

    public DbSet<Order> Orders { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(Consts.DatabaseConnectionString);
        base.OnConfiguring(optionsBuilder);
    }
}