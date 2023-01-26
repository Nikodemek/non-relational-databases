using Cinema.Entity;
using Cinema.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.Controllers;

[ApiController]
[Route("/Movies")]
public class MovieController : ControllerBase
{
    private readonly ILogger<MovieController> _logger;
    private readonly IMovieService _movieService;

    public MovieController(ILogger<MovieController> logger, IMovieService movieService)
    {
        _logger = logger;
        _movieService = movieService;
    }
    
    [HttpGet]
    public async Task<IEnumerable<Movie>> GetAll() => await _movieService.GetAllAsync();

    [HttpGet("{id}")]
    public async Task<Movie> Get(string id) => await _movieService.GetAsync(id);

    [HttpPost]
    public async Task Register([FromBody] Movie newMovie) => await _movieService.CreateAsync(newMovie);
    
    [HttpDelete("{id}")]
    public async Task Remove(string id) => await _movieService.RemoveAsync(id);
}