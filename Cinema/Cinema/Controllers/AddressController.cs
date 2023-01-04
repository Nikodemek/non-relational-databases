using Cinema.Models;
using Cinema.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.Controllers;

[ApiController]
[Route("/Addresses")]
public class AddressController : ControllerBase
{
    private readonly ILogger<AddressController> _logger;
    private readonly IAddresses _addresses;

    public AddressController(ILogger<AddressController> logger, IAddresses addresses)
    {
        _logger = logger;
        _addresses = addresses;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await _addresses.GetAllAsync());

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get(Guid id) => Ok(await _addresses.GetAsync(id));

    [HttpPost("Register")]
    public async Task<IActionResult> Register([FromBody] Address newAddress) => Ok(await _addresses.CreateAsync(newAddress));
    
    [HttpDelete("Remove/{id:guid}")]
    public async Task<IActionResult> Remove(Guid id) => Ok(await _addresses.DeleteAsync(id));
}