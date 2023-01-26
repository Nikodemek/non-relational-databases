namespace Cinema.Entity.Utils;

internal static class Generate
{
    public static string Id()
    {
        var guid = Guid.NewGuid();
        return guid.ToString();
    }
}