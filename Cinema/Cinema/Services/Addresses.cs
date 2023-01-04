using Cinema.Mappers.Interfaces;
using Cinema.Models;
using Cinema.Models.Dto;
using Cinema.Services.Interfaces;

namespace Cinema.Services;

public class Addresses : UniversalCommonsService<Address, AddressDto>, IAddresses
{
    public Addresses(ILogger<Addresses> logger, IEntityMapper<Address, AddressDto> mapper)
        : base(logger, mapper)
    { }
}