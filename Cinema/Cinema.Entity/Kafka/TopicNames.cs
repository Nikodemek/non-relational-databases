namespace Cinema.Entity.Kafka;

public class TopicNames
{
    public static readonly TopicNames Default = new();
    
    public string CinemaOrders { get; init; } = "NBD-Cinema-Orders";
}