using Cinema.Entity;
using Cinema.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.Controllers;

[ApiController]
[Route("/Screenings")]
public class ScreeningController : ControllerBase
{
    private readonly ILogger<ScreeningController> _logger;
    private readonly IScreeningService _screeningService;

    public ScreeningController(ILogger<ScreeningController> logger, IScreeningService screeningService)
    {
        _logger = logger;
        _screeningService = screeningService;
    }
    
    [HttpGet]
    public async Task<IEnumerable<Screening>> GetAll() => await _screeningService.GetAllAsync();

    [HttpGet("{id}")]
    public async Task<Screening> Get(string id) => await _screeningService.GetAsync(id);

    [HttpPost("Register")]
    public async Task Register([FromBody] Screening newScreening) => await _screeningService.CreateAsync(newScreening);
    
    [HttpDelete("Remove/{id}")]
    public async Task Remove(string id) => await _screeningService.RemoveAsync(id);
}