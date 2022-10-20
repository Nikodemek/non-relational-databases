using Cinema;
using Cinema.Data;
using Cinema.Models;

using CinemaContext context = new CinemaContext();

var ticket = context.Tickets
    .FirstOrDefault(t => t.Price > 16);

Console.WriteLine(ticket);