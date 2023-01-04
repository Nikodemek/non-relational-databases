using Cinema.Models;
using Cinema.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.Controllers;

[ApiController]
[Route("/Screenings")]
public class ScreeningController : ControllerBase
{
    private readonly ILogger<ScreeningController> _logger;
    private readonly IScreenings _screenings;

    public ScreeningController(ILogger<ScreeningController> logger, IScreenings screenings)
    {
        _logger = logger;
        _screenings = screenings;
    }
    
    [HttpGet]
    public async Task<IEnumerable<Screening>> GetAll() => await _screenings.GetAllAsync();

    [HttpGet("{id:guid}")]
    public async Task<Screening?> Get(Guid id) => await _screenings.GetAsync(id);

    [HttpPost("Register")]
    public async Task Register([FromBody] Screening newScreening) => await _screenings.CreateAsync(newScreening);
    
    [HttpDelete("Remove/{id:guid}")]
    public async Task Remove(Guid id) => await _screenings.DeleteAsync(id);
}