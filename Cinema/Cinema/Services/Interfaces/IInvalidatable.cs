namespace Cinema.Services.Interfaces;

public interface IInvalidatable
{
    Task DeleteAllAsync(bool fullReset = false);
    Task RestoreAllAsync();
}