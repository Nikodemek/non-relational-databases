using Cinema.Models;
using Cinema.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;

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

    [HttpGet("{id}")]
    public async Task<Client?> Get(string id) => await _clients.GetAsync(id);

    [HttpPost("Register")]
    public async Task Register([FromBody] Client newClient) => await _clients.CreateAsync(newClient);

    [HttpPut("Update")]
    public async Task Update([FromBody] Client updatedClient) => await _clients.UpdateAsync(updatedClient);

    [HttpPut("Archive/{id}")]
    public async Task Archive(string id) => await _clients.ArchiveAsync(id);

    [HttpDelete("Remove/{id}")]
    public async Task Remove(string id) => await _clients.DeleteAsync(id);

}