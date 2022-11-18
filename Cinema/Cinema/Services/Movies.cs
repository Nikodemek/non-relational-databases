using Cinema.Models;
using Cinema.Services.Interfaces;

namespace Cinema.Services;

public sealed class Movies : Commons<Movies, Movie>, IMovies
{ }