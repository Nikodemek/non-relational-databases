using Cassandra.Mapping;
using Cinema.Data;
using Cinema.Mappers;

namespace Cinema.Tests;

public static class DbManager
{
    private static bool _initialized = false;

    public static void Initialize()
    {
        if (_initialized) return;
        
        CinemaDb.SetUpConnection("127.0.0.1", new[] {9142, 9242});
        MappingConfiguration.Global.Define<CassandraMappings>();
        
        _initialized = true;
    }
}