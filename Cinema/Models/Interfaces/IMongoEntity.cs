namespace Cinema.Models.Interfaces;

public interface IMongoEntity<T>
{
    int Id { get; set; }
}