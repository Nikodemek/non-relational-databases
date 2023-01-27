using System.Collections.Generic;
using System.Threading.Tasks;
using Cinema.Entity;
using Cinema.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;

namespace Cinema.Controllers;

[ApiController]
[Route("/Clients")]
public class ClientController : ControllerBase
{
    private readonly ILogger<ClientController> _logger;
    private readonly IClientService _clientService;

    public ClientController(ILogger<ClientController> logger, IClientService clientService)
    {
        _logger = logger;
        _clientService = clientService;
    }

    [HttpGet]
    public async Task<IEnumerable<Client>> GetAll() => await _clientService.GetAllAsync();

    [HttpGet("{id}")]
    public async Task<Client> Get(string id) => await _clientService.GetAsync(id);

    [HttpPost]
    public async Task Register([FromBody] Client newClient) => await _clientService.CreateAsync(newClient);

    [HttpPut("{id}")]
    public async Task Update(string id, [FromBody] Client updatedClient) => await _clientService.UpdateAsync(id, updatedClient);

    [HttpPut("Archive/{id}")]
    public async Task<ReplaceOneResult> Archive(string id) => await _clientService.ArchiveAsync(id);

    [HttpDelete("{id}")]
    public async Task Remove(string id) => await _clientService.RemoveAsync(id);

}