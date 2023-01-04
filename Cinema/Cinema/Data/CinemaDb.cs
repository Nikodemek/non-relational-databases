using System.Net;
using System.Net.NetworkInformation;
using System.Security.Principal;
using Cassandra;
using Cassandra.Mapping;
using Cinema.Models;
using ISession = Cassandra.ISession;

namespace Cinema.Data;

public static class CinemaDb
{
    public const string AppName = "Cinema";
    public static readonly string Keyspace = AppName.ToLower();
    
    private static ICluster? _cluster;
    private static ISession? _session;
    private static IMapper? _mapper;

    public static IMapper Database => _mapper ?? throw new Exception("Connection not initialized!");
    public static ISession Session => _session ?? throw new Exception("Connection not initialized!");

    public static void SetUpConnection(IConfiguration configuration)
    {
        ReadOnlySpan<char> hostIpAddress = "127.0.0.1";
        IPAddress host = IPAddress.Parse(hostIpAddress);
        int node1Port = 9142;
        int node2Port = 9242;

        _cluster = Cluster.Builder()
            .AddContactPoint(new IPEndPoint(host, node1Port))
            .AddContactPoint(new IPEndPoint(host, node2Port))
            .WithReconnectionPolicy(new ConstantReconnectionPolicy(1000))
            .WithDefaultKeyspace(Keyspace)
            .WithApplicationName(AppName)
            .Build();
        
        _session = _cluster.Connect(Keyspace);

        _mapper = new Mapper(_session);
    }
}