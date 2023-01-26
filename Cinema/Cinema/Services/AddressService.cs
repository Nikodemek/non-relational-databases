using Cinema.Entity;
using Cinema.Repository.Interfaces;
using Cinema.Services.Interfaces;

namespace Cinema.Services;

public class AddressService : CommonService<Address>, IAddressService
{
    public AddressService(IAddressesRepository clientRepository)
        : base(clientRepository)
    { }
}