using Cinema.Models;
using Cinema.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.Controllers;

[ApiController]
[Route("/Movies")]
public class MovieController : ControllerBase
{
    private readonly ILogger<MovieController> _logger;
    private readonly IMovies _movies;

    public MovieController(ILogger<MovieController> logger, IMovies movies)
    {
        _logger = logger;
        _movies = movies;
    }
    
    [HttpGet]
    public async Task<IEnumerable<Movie>> GetAll() => await _movies.GetAllAsync();

    [HttpGet("{id:int}")]
    public async Task<Movie> Get(int id) => await _movies.GetAsync(id);

    [HttpPost("Register")]
    public async Task Register(Movie newMovie) => await _movies.CreateAsync(newMovie);
}