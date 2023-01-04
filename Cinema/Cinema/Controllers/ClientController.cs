using Cinema.Models;
using Cinema.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.Controllers;

[ApiController]
[Route("/Clients")]
public class ClientController : ControllerBase
{
    private readonly ILogger<ClientController> _logger;
    private readonly IClients _clients;

    public ClientController(ILogger<ClientController> logger, IClients clients)
    {
        _logger = logger;
        _clients = clients;
    }

    [HttpGet]
    public async Task<IEnumerable<Client>> GetAll() => await _clients.GetAllAsync();

    [HttpGet("{id:guid}")]
    public async Task<Client?> Get(Guid id) => await _clients.GetAsync(id);

    [HttpPost("Register")]
    public async Task Register([FromBody] Client newClient) => await _clients.CreateAsync(newClient);

    [HttpPut("Update")]
    public async Task Update([FromBody] Client updatedClient) => await _clients.UpdateAsync(updatedClient);

    [HttpPut("Archive/{id:guid}")]
    public async Task Archive(Guid id) => await _clients.ArchiveAsync(id);

    [HttpDelete("Remove/{id:guid}")]
    public async Task Remove(Guid id) => await _clients.DeleteAsync(id);

}