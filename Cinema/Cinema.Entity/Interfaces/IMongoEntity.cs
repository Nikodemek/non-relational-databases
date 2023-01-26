namespace Cinema.Entity.Interfaces;

public interface IMongoEntity<T>
{
    string Id { get; set; }
}