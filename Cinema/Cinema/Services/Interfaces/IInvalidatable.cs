using Cinema.Models.Interfaces;

namespace Cinema.Services.Interfaces;

public interface IInvalidatable
{
    Task DeleteAllAsync();
    Task RestoreAllAsync();
}