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
    public async Task<IActionResult> GetAll() => Ok(await _screeningService.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(string id) => Ok(await _screeningService.GetAsync(id));

    [HttpPost]
    public async Task Register([FromBody] Screening newScreening) => await _screeningService.CreateAsync(newScreening);
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Remove(string id) => Ok(await _screeningService.RemoveAsync(id));
}