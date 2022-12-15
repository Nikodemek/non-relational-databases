using Cinema.Services.Interfaces;

namespace Cinema.Services;

public static class ValidityService
{
    private static readonly HashSet<IInvalidatable> Invalidatables = new();

    public static bool RegisterInvalidatable(IInvalidatable invalidatable)
    {
        return Invalidatables.Add(invalidatable);
    }
    
    public static bool UnregisterInvalidatable(IInvalidatable invalidatable)
    {
        return Invalidatables.Remove(invalidatable);
    }

    public static async Task InvalidateAll()
    {
        var deletionTasks = Invalidatables.Select(invalidatable => invalidatable.DeleteAllAsync());
        await Task.WhenAll(deletionTasks);
        
        var restorationTasks = Invalidatables.Select(invalidatable => invalidatable.RestoreAllAsync());
        await Task.WhenAll(restorationTasks);
    }
}