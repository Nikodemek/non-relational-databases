using Cinema.Entity;
using Cinema.Repository.Interfaces;
using Cinema.Services.Interfaces;

namespace Cinema.Services;

public class AddressService : CommonService<Address>, IAddressService
{
    public AddressService(ICommonsRepository<Address> clientRepository)
        : base(clientRepository)
    { }
}