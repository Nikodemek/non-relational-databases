using Cinema.Models;

namespace Cinema;

public static class TestData
{
    public static readonly Movie Movie1 = new()
    {
        Title = "Piłaci z Kałaibów",
        AgeCategory = AgeCategory.G,
        Length = 120
    } ;
    
    public static readonly Screening Screening1 = new()
    {
        Movie = Movie1,
        Time = DateTime.Now + TimeSpan.FromHours(1)
    };
    
    public static readonly Ticket Ticket1 = new()
    {
        Screening = Screening1,
        Sold = false,
        Archived = false,
        Price = 15
    };
    
    public static readonly Ticket Ticket2 = new()
    {
        Screening = Screening1,
        Sold = false,
        Archived = false,
        Price = 20
    };
    
    public static readonly Address Address1 = new()
    {
        Country = "Polska",
        City = "Pabianice",
        Street = "Długa",
        Number = "13/15"
    };
    
    public static readonly Client Client1 = new()
    {
        FirstName = "Kowl",
        LastName = "Jankowski",
        Birthday = DateTime.Parse("1994-06-12 12:00:00"),
        Address = Address1,
        PhoneNumber = "123 456 789",
        ClientType = ClientType.Default,
        AccountBalance = 120,
        Archived = false
    };
    
    public static readonly Order Order1 = new(new [] { Ticket1, Ticket2 })
    {
        Client = Client1,
        PlacedTime = DateTime.Now
    };
}