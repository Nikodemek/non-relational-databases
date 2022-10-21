using Cinema.Models;
using Cinema.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.Controllers;

[ApiController]
[Route("/Tickets")]
public class TicketController : ControllerBase
{
    private readonly ILogger<TicketController> _logger;
    private readonly ITicketService _ticketService;

    public TicketController(ILogger<TicketController> logger, ITicketService ticketService)
    {
        _logger = logger;
        _ticketService = ticketService;
    }
    
    [HttpGet]
    public IEnumerable<Ticket> GetAll() => _ticketService.GetAll();

    [HttpGet("{id:int}")]
    public Ticket? Get(int id) => _ticketService.Get(id);

    [HttpPost("Register")]
    public Ticket? Register(Ticket newTicket) => _ticketService.Create(newTicket);
}