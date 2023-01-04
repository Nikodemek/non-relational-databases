using Cinema.Data;
using Cinema.Models;
using Cinema.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;

namespace Cinema.Services;

public class Addresses : UniversalCommonsService<Address>, IAddresses
{
    public Addresses(ILogger<Addresses> logger)
        : base(logger)
    { }
}