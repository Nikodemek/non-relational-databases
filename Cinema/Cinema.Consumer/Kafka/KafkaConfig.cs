using Cinema.Entity.Kafka;
using Confluent.Kafka;

namespace Cinema.Consumer.Kafka;

public record KafkaConfig()
{
    public string GroupId { get; set; } = "Cinema-ConsumerGroup";
    public string BootstrapServers { get; set; } = "localhost:9092";
    public AutoOffsetReset AutoOffsetReset { get; set; } = AutoOffsetReset.Earliest;
}