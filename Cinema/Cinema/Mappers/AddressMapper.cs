using Cinema.Mappers.Interfaces;
using Cinema.Models;
using Cinema.Models.Dto;

namespace Cinema.Mappers;

public class AddressMapper : IEntityMapper<Address, AddressDto>
{
    public Task<Address> ToEntity(AddressDto dto)
    {
        return Task.FromResult(new Address()
        {
            Id = dto.Id,
            Country = dto.Country,
            City = dto.City,
            Street = dto.Street,
            Number = dto.Number
        });
    }

    public Task<AddressDto> ToDto(Address entity)
    {
        return Task.FromResult(new AddressDto()
        {
            Id = entity.Id,
            Country = entity.Country,
            City = entity.City,
            Street = entity.Street,
            Number = entity.Number
        });
    }
}