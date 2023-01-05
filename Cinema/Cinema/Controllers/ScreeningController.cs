using Cinema.Models;
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

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get(Guid id) => Ok(await _screeningService.GetAsync(id));

    [HttpPost("Register")]
    public async Task<IActionResult> Register([FromBody] Screening newScreening) => Ok(await _screeningService.CreateAsync(newScreening));
    
    [HttpDelete("Remove/{id:guid}")]
    public async Task<IActionResult> Remove(Guid id) => Ok(await _screeningService.DeleteAsync(id));
}