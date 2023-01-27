using System.Threading.Tasks;
using Cinema.Entity;
using Cinema.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Cinema.Controllers;

[ApiController]
[Route("/Addresses")]
public class AddressController : ControllerBase
{
    private readonly ILogger<AddressController> _logger;
    private readonly IAddressService _addressService;

    public AddressController(ILogger<AddressController> logger, IAddressService addressService)
    {
        _logger = logger;
        _addressService = addressService;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await _addressService.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(string id) => Ok(await _addressService.GetAsync(id));

    [HttpPost]
    public async Task Register([FromBody] Address newAddress) => await _addressService.CreateAsync(newAddress);
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Remove(string id) => Ok(await _addressService.RemoveAsync(id));
}