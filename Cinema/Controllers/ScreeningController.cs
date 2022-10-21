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
    public IEnumerable<Screening> GetAll() => _screeningService.GetAll();

    [HttpGet("{id:int}")]
    public Screening? Get(int id) => _screeningService.Get(id);

    [HttpGet("Register")]
    public Screening? Register(Screening newScreening) => _screeningService.Create(newScreening);
}