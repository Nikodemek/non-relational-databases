using Cinema.Models;
using Cinema.Services.Interfaces;

namespace Cinema.Services;

public sealed class Screenings : Commons<Screenings, Screening>, IScreenings
{ }