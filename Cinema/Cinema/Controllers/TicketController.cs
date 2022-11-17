using Cinema.Models;
using Cinema.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

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

    [HttpGet("{id}")]
    public async Task<Ticket> Get(ObjectId id) => await _tickets.GetAsync(id);

    [HttpPost("Register")]
    public async Task Register([FromBody] Ticket newTicket) => await _tickets.CreateAsync(newTicket);
    
    [HttpDelete("Remove/{id}")]
    public async Task Remove(ObjectId id) => await _tickets.RemoveAsync(id);
}