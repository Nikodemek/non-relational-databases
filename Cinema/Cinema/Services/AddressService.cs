using Cinema.Mappers.Interfaces;
using Cinema.Models;
using Cinema.Models.Dto;
using Cinema.Services.Interfaces;

namespace Cinema.Services;

public class AddressService : CommonService<Address, AddressDto>, IAddressService
{
    public AddressService(ILogger<AddressService> logger, IEntityMapper<Address, AddressDto> mapper)
        : base(logger, mapper)
    { }
}