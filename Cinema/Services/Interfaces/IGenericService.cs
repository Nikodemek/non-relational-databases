namespace Cinema.Services.Interfaces;

public interface IGenericService<T>
{
    IEnumerable<T> GetAll();
    T? Get(int id);
    T? Create(T entity);
}