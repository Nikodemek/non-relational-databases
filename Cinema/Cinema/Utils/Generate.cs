using Cassandra;

namespace Cinema.Utils;

public static class Generate
{
    public static Guid Id()
    {
        return TimeUuid.NewId();
    }
}