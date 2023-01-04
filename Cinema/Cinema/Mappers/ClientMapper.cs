using Cinema.Mappers.Interfaces;
using Cinema.Models;
using Cinema.Models.Dto;
using Cinema.Services.Interfaces;

namespace Cinema.Mappers;

public class ClientMapper : IEntityMapper<Client, ClientDto>
{
    private readonly IAddresses _addresses;

    public ClientMapper(IAddresses addresses)
    {
        _addresses = addresses;
    }

    public async Task<Client> ToEntity(ClientDto dto)
    {
        return new Client()
        {
            Id = dto.Id,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Birthday = dto.Birthday,
            ClientType = (ClientType) dto.ClientType,
            Address = await _addresses.GetAsync(dto.AddressId),
            AccountBalance = dto.AccountBalance,
            Archived = dto.Archived,
        };
    }

    public Task<ClientDto> ToDto(Client entity)
    {
        return Task.FromResult(new ClientDto()
        {
            Id = entity.Id,
            FirstName = entity.FirstName,
            LastName = entity.LastName,
            Birthday = entity.Birthday,
            ClientType = (int) entity.ClientType,
            AddressId = entity.Address?.Id ?? Guid.Empty,
            AccountBalance = entity.AccountBalance,
            Archived = entity.Archived,
        });
    }
}