﻿using System.Text.Json;
using Cinema.Consumer.Kafka;
using Cinema.Consumer.Kafka.Interfaces;
using Cinema.Entity;
using Cinema.Entity.Kafka;
using Cinema.Repository;
using Cinema.Repository.Interfaces;


CinemaConnection.Configure(args);

KafkaConfig kafkaConfig = ArgsParser.ParseKafkaConfig(args);
ICommonsRepository<Order> ordersRepository = new OrdersRepository();
string topic = KafkaConsts.TopicNames.CinemaOrders;

IKafkaConsumer<Order> kafkaConsumer = new KafkaConsumer<Order>(kafkaConfig, ordersRepository, topic);

try
{
    var cancellationToken = new CancellationTokenSource();

    Task.Run(() =>
    {
        while (true)
        {
            string? input = Console.ReadLine();
            if (String.Equals(input, "q", StringComparison.OrdinalIgnoreCase))
            {
                cancellationToken.Cancel();
            }
        }
    });

    while (!cancellationToken.IsCancellationRequested)
    {
        Console.WriteLine("Waiting for message...");

        var fullResponse = await kafkaConsumer.ConsumeSingleAsync(cancellationToken.Token);

        Console.WriteLine(JsonSerializer.Serialize(fullResponse, KafkaConsts.JsonConsumerOptions));
        Console.WriteLine();
    }
}
catch (OperationCanceledException)
{
    Console.WriteLine("Thanks for using the app! Cya!");
}
catch (Exception e)
{
    Console.WriteLine(e);
    throw;
}
finally
{
    kafkaConsumer.Dispose();
}