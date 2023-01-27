namespace Cinema.Entity.Kafka;

public class HeaderNames
{
    public static readonly HeaderNames Default = new();
    
    public string CinemaName { get; init; } = "CinemaName";
    public string Type { get; init; } = "Type";
}