using System.Net;
using Cassandra;
using Cassandra.Mapping;
using ISession = Cassandra.ISession;

namespace Cinema.Data;

public static class CinemaDb
{
    public const string AppName = "Cinema";
    public static readonly string Keyspace = AppName.ToLower();
    
    private static ICluster? _cluster;
    private static ISession? _session;

    public static ISession Session => _session ?? throw new ArgumentException("Connection not initialized!");

    public static void SetUpConnection(IConfiguration configuration)
    {
        string hostIpAddress = configuration
            .GetConnectionString("Cassandra");
        IEnumerable<int> ports = configuration
            .GetSection("CassandraNodePorts")
            .Get<IEnumerable<int>>();
        
        SetUpConnection(hostIpAddress, ports);
    }

    public static void SetUpConnection(string hostIpAddress, IEnumerable<int> ports)
    {
        IPAddress host = IPAddress.Parse(hostIpAddress);
        IEnumerable<IPEndPoint> endpoints = ports.Select(port => new IPEndPoint(host, port));
        
        _cluster = Cluster.Builder()
            .AddContactPoints(endpoints)
            .WithReconnectionPolicy(new ConstantReconnectionPolicy(1000))
            .WithDefaultKeyspace(Keyspace)
            .WithApplicationName(AppName)
            .Build();

        _session = _cluster.Connect();
    }
}