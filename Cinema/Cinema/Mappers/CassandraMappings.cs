using Cassandra;
using Cassandra.Mapping;
using Cinema.Models;
using Cinema.Models.Dto;
using Cinema.Models.Interfaces;

namespace Cinema.Mappers;

public class CassandraMappings : Mappings
{
    public CassandraMappings()
    {
        CreateForAddress();
        CreateForClient();
        CreateForMovie();
        CreateForScreening();
        CreateForTicket();
        CreateForOrder();
    }

    private Map<AddressDto> CreateForAddress()
    {
        return For<AddressDto>().TableName(nameof(Address))
            .Column(x => x.Id)
            .Column(x => x.Country)
            .Column(x => x.City)
            .Column(x => x.Street)
            .Column(x => x.Number)
            .ClusteringKey(x => x.Country)
            .PartitionKey(x => x.Id);
    }

    private Map<ClientDto> CreateForClient()
    {
        return For<ClientDto>().TableName(nameof(Client))
            .Column(x => x.Id)
            .Column(x => x.FirstName)
            .Column(x => x.LastName)
            .Column(x => x.Birthday)
            .Column(x => x.ClientType)
            .Column(x => x.AddressId)
            .Column(x => x.AccountBalance)
            .Column(x => x.Archived)
            .ClusteringKey(x => x.ClientType)
            .PartitionKey(x => x.Id);
    }

    private Map<MovieDto> CreateForMovie()
    {
        return For<MovieDto>().TableName(nameof(Movie))
            .Column(x => x.Id)
            .Column(x => x.Title)
            .Column(x => x.Length)
            .Column(x => x.AgeCategory)
            .ClusteringKey(x => x.Title)
            .PartitionKey(x => x.Id);
    }

    private Map<ScreeningDto> CreateForScreening()
    {
        return For<ScreeningDto>().TableName(nameof(Screening))
            .Column(x => x.Id)
            .Column(x => x.MovieId)
            .Column(x => x.Time)
            .ClusteringKey(x => x.Id)
            .PartitionKey(x => x.Id);
    }

    private Map<TicketDto> CreateForTicket()
    {
        return For<TicketDto>().TableName(nameof(Ticket))
            .Column(x => x.Id)
            .Column(x => x.Price)
            .Column(x => x.ScreeningId)
            .Column(x => x.Sold)
            .Column(x => x.Archived)
            .ClusteringKey(x => x.Archived)
            .PartitionKey(x => x.Id);
    }

    private Map<OrderDto> CreateForOrder()
    {
        return For<OrderDto>().TableName(nameof(Order))
            .Column(x => x.Id)
            .Column(x => x.ClientId)
            .Column(x => x.PlacedTime)
            .Column(x => x.FinalPrice)
            .Column(x => x.Success)
            .Column(x => x.TicketIds)
            .ClusteringKey(x => x.PlacedTime)
            .PartitionKey(x => x.Id);
    }
}