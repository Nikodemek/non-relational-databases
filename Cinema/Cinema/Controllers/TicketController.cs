using Cinema.Entity;
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
    public async Task<IEnumerable<Ticket>> GetAll() => await _ticketService.GetAllAsync();

    [HttpGet("{id}")]
    public async Task<Ticket> Get(string id) => await _ticketService.GetAsync(id);

    [HttpPost("")]
    public async Task Register([FromBody] Ticket newTicket) => await _ticketService.CreateAsync(newTicket);

    [HttpPut]
    public async Task Update(string id, [FromBody] Ticket updatedTicket) => await _ticketService.UpdateAsync(id, updatedTicket);
    
    [HttpDelete("{id}")]
    public async Task Remove(string id) => await _ticketService.RemoveAsync(id);
}