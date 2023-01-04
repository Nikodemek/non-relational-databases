create keyspace cinema with replication = {
    'class': 'SimpleStrategy',
    'replication_factor': 1
};
