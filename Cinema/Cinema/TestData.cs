using Cinema.Models;
using Cinema.Services.Interfaces;
using Cinema.Utils;

namespace Cinema;

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
        await _addressService.CreateAsync(Address1 with {Id = Generate.Id()});
        await _clientService.CreateAsync(Client1 with {Id = Generate.Id()});
        await _screeningService.CreateAsync(Screening1 with {Id = Generate.Id()});
        await _movieService.CreateAsync(Movie1 with {Id = Generate.Id()});
        await _ticketService.CreateAsync(Ticket2 with {Id = Generate.Id()});
        await _orderService.CreateAsync(Order1 with {Id = Generate.Id()});
    }

    public async void DeleteData()
    {
        await _addressService.DeleteAsync(Address1.Id);
        await _clientService.DeleteAsync(Client1.Id);
        await _screeningService.DeleteAsync(Screening1.Id);
        await _movieService.DeleteAsync(Movie1.Id);
        await _ticketService.DeleteAsync(Ticket1.Id);
        await _ticketService.DeleteAsync(Ticket2.Id);
        await _orderService.DeleteAsync(Order1.Id);
    }
}