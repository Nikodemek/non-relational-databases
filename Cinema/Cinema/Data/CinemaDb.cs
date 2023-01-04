﻿using System.Net;
using System.Net.NetworkInformation;
using System.Security.Principal;
using Cassandra;
using Cassandra.Mapping;
using ISession = Cassandra.ISession;

namespace Cinema.Data;

public static class CinemaDb
{
    private static ICluster? _cluster;
    private static ISession? _session;
    private static IMapper? _mapper;

    public static IMapper Db => _mapper ?? throw new Exception("Connection not initialized!");

    public static IMapper SetUpConnection(IConfiguration configuration)
    {
        const string keyspace = "cinema";
        
        ReadOnlySpan<char> hostIpAddress = "127.0.0.1";
        IPAddress host = IPAddress.Parse(hostIpAddress);
        int node1Port = 9139;
        int node2Port = 9240;

        _cluster = Cluster.Builder()
            .AddContactPoint(new IPEndPoint(host, node1Port))
            .AddContactPoint(new IPEndPoint(host, node2Port))
            .WithReconnectionPolicy(new ConstantReconnectionPolicy(1000))
            .Build();
        
        _session = _cluster.Connect(keyspace);
        
        return _mapper = new Mapper(_session);
    }
}