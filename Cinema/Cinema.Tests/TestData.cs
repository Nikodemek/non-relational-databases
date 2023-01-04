using System;
using Cinema.Models;
using Cinema.Services.Interfaces;

namespace Cinema.Tests;

public class TestData
{
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

    private readonly IAddressService _addressService;
    private readonly IClientService _clientService;
    private readonly IMovieService _movieService;
    private readonly IOrderService _orderService;
    private readonly IScreeningService _screeningService;
    private readonly ITicketService _ticketService;
    
    public TestData(IAddressService addressService, IClientService clientService, IMovieService movieService, IOrderService orderService, IScreeningService screeningService, ITicketService ticketService)
    {
        _addressService = addressService;
        _clientService = clientService;
        _movieService = movieService;
        _orderService = orderService;
        _screeningService = screeningService;
        _ticketService = ticketService;
    }

    public async void InsertData()
    {
        await _addressService.CreateAsync(Address1);
        await _clientService.CreateAsync(Client1);
        await _screeningService.CreateAsync(Screening1);
        await _movieService.CreateAsync(Movie1);
        await _ticketService.CreateAsync(Ticket1);
        await _ticketService.CreateAsync(Ticket2);
        await _orderService.CreateAsync(Order1);
    }
}