using Cinema.Data;
using Cinema.Models;
using Cinema.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Services;

public class AddressService : IAddressService
{
    private readonly CinemaContext _context;
    private readonly DbSet<Address> _addresses;

    public AddressService(CinemaContext context)
    {
        _context = context;
        _addresses = context.Addresses;
    }

    public IEnumerable<Address> GetAll()
    {
        return _addresses;
    }

    public Address? Get(int id)
    {
        return _addresses
            .FirstOrDefault(a => a.Id == id);
    }

    public Address? Create(Address address)
    {
        var addedAddress = _addresses.Add(address);
        _context.SaveChanges();
        return addedAddress.Entity;
    }
}