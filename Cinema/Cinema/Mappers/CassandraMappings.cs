using Cassandra.Mapping;
using Cinema.Models;
using Cinema.Models.Interfaces;

namespace Cinema.Mappers;

public class CassandraMappings : Mappings
{
    public CassandraMappings()
    {
        CreateForAddress();
        CreateForClient();
        CreateForMovie();
        CreateForOrder();
        CreateForScreening();
        CreateForTicket();
    }

    private Map<Address> CreateForAddress()
    {
        return For<Address>().TableName(nameof(Address))
            .Column(x => x.Id)
            .Column(x => x.Country)
            .Column(x => x.City)
            .Column(x => x.Street)
            .Column(x => x.Number)
            .PartitionKey(x => x.Id);
    }

    private Map<Client> CreateForClient()
    {
        return For<Client>().TableName(nameof(Client))
            .Column(x => x.Id)
            .Column(x => x.FirstName)
            .Column(x => x.LastName)
            .Column(x => x.Birthday)
            .Column(x => x.ClientType)
            .Column(x => x.Address)
            .Column(x => x.AccountBalance)
            .Column(x => x.Archived)
            .PartitionKey(x => x.Id);
    }

    private Map<Movie> CreateForMovie()
    {
        return For<Movie>().TableName(nameof(Movie))
            .Column(x => x.Id)
            .Column(x => x.Title)
            .Column(x => x.Length)
            .Column(x => x.AgeCategory)
            .PartitionKey(x => x.Id);
    }

    private Map<Order> CreateForOrder()
    {
        return For<Order>().TableName(nameof(Order))
            .Column(x => x.Id)
            .Column(x => x.Client)
            .Column(x => x.PlacedTime)
            .Column(x => x.FinalPrice)
            .Column(x => x.Success)
            .Column(x => x.Tickets)
            .PartitionKey(x => x.Id);
    }

    private Map<Screening> CreateForScreening()
    {
        return For<Screening>().TableName(nameof(Screening))
            .Column(x => x.Id)
            .Column(x => x.Movie)
            .Column(x => x.Time)
            .PartitionKey(x => x.Id);
    }

    private Map<Ticket> CreateForTicket()
    {
        return For<Ticket>().TableName(nameof(Ticket))
            .Column(x => x.Id)
            .Column(x => x.Price)
            .Column(x => x.Screening)
            .Column(x => x.Sold)
            .Column(x => x.Archived)
            .PartitionKey(x => x.Id);
    }
}