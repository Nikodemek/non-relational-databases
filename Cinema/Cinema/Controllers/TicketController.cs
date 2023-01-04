using Cinema.Models;
using Cinema.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.Controllers;

[ApiController]
[Route("/Tickets")]
public class TicketController : ControllerBase
{
    private readonly ILogger<TicketController> _logger;
    private readonly ITickets _tickets;

    public TicketController(ILogger<TicketController> logger, ITickets tickets)
    {
        _logger = logger;
        _tickets = tickets;
    }
    
    [HttpGet]
    public async Task<IEnumerable<Ticket>> GetAll() => await _tickets.GetAllAsync();

    [HttpGet("{id:guid}")]
    public async Task<Ticket?> Get(Guid id) => await _tickets.GetAsync(id);

    [HttpPost("Register")]
    public async Task Register([FromBody] Ticket newTicket) => await _tickets.CreateAsync(newTicket);
    
    [HttpDelete("Remove/{id:guid}")]
    public async Task Remove(Guid id) => await _tickets.DeleteAsync(id);
}