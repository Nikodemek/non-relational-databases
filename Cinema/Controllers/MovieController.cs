using Cinema.Models;
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
    public IEnumerable<Movie> GetAll() => _movieService.GetAll();

    [HttpGet("{id:int}")]
    public Movie? Get(int id) => _movieService.Get(id);

    [HttpGet("Register")]
    public Movie? Register(Movie newMovie) => _movieService.Create(newMovie);
}