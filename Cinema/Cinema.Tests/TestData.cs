using System;
using Cinema.Models;
using Cinema.Services.Interfaces;
using Microsoft.Extensions.Caching.StackExchangeRedis;

namespace Cinema.Tests;

public class TestData
{
    public static readonly RedisCacheOptions RedisCacheOptions = new ()
    {
        Configuration = "localhost:6379",
        InstanceName = "Cinema_Tests_",
    };
    
    public static readonly Movie Movie1 = new()
    {
        Title = "Piłaci z Kałaibów",
        AgeCategory = AgeCategory.G,
        Length = 120,
    } ;
    
    public static readonly Screening Screening1 = new()
    {
        Movie = Movie1,
        Time = DateTime.Now + TimeSpan.FromHours(1)
    };
    
    public static readonly Ticket Ticket1 = new()
    {
        Screening = Screening1,
        Sold = false,
        Archived = false,
        Price = 15,
    };
    
    public static readonly Ticket Ticket2 = new()
    {
        Screening = Screening1,
        Sold = false,
        Archived = false,
        Price = 20,
    };
    
    public static readonly Address Address1 = new()
    {
        Country = "Polska",
        City = "Pabianice",
        Street = "Długa",
        Number = "13/15",
    };
    
    public static readonly Client Client1 = new()
    {
        FirstName = "Kowl",
        LastName = "Jankowski",
        Birthday = DateTime.Parse("1994-06-12 12:00:00"),
        Address = Address1,
        ClientType = ClientType.Default,
        AccountBalance = 120,
        Archived = false,
    };
    
    public static readonly Order Order1 = new(new [] { Ticket1, Ticket2 })
    {
        Client = Client1,
        PlacedTime = DateTime.Now,
    };

    private readonly IAddresses _addresses;
    private readonly IClients _clients;
    private readonly IMovies _movies;
    private readonly IOrders _orders;
    private readonly IScreenings _screenings;
    private readonly ITickets _tickets;
    
    public TestData(IAddresses addresses, IClients clients, IMovies movies, IOrders orders, IScreenings screenings, ITickets tickets)
    {
        _addresses = addresses;
        _clients = clients;
        _movies = movies;
        _orders = orders;
        _screenings = screenings;
        _tickets = tickets;
    }

    public async void InsertData()
    {
        await _addresses.CreateAsync(Address1);
        await _clients.CreateAsync(Client1);
        await _screenings.CreateAsync(Screening1);
        await _movies.CreateAsync(Movie1);
        await _tickets.CreateAsync(Ticket1);
        await _tickets.CreateAsync(Ticket2);
        await _orders.CreateAsync(Order1);
    }
}