using System.Text.Json;
using Cinema.Entity.Interfaces;
using Cinema.Entity.Kafka;
using Cinema.Kafka.Interfaces;
using Cinema.Repository;
using Confluent.Kafka;

namespace Cinema.Kafka;

internal class KafkaProducer<TEntity> : IKafkaProducer<TEntity>
    where TEntity : IMongoEntity<TEntity>
{
    private readonly Header _cinemaNameHeader = new(
        KafkaConsts.HeaderNames.CinemaName,
        KafkaConsts.Encoding.GetBytes(Consts.CinemaName)
    );
    private readonly Header _orderTypeHeader = new(
        KafkaConsts.HeaderNames.Type,
        KafkaConsts.Encoding.GetBytes(typeof(TEntity).FullName ?? string.Empty)
    );
    private readonly IProducer<string, string> _producer;

    public KafkaProducer(IConfiguration configuration)
    {
        var kafkaConfigurationSection = configuration.GetSection("Kafka");
        
        var config = new ProducerConfig()
        {
            BootstrapServers = kafkaConfigurationSection.GetValue<string?>("BootstrapServers") 
                ?? throw new Exception("Could not read 'BootstrapServers' from Configuration!"),
            MessageMaxBytes = kafkaConfigurationSection.GetValue<int?>("MessageMaxBytes")
                ?? throw new Exception("Could not read 'MessageMaxBytes' from Configuration!"),
        };

        var producerBuilder = new ProducerBuilder<string, string>(config);

        _producer = producerBuilder.Build();
    }
    
    public Task<DeliveryResult<string, string>> ProduceAsync(TEntity entity)
    {
        var serializedOrder = JsonSerializer.Serialize(entity, KafkaConsts.JsonProducerOptions);

        var message = new Message<string, string>()
        {
            Key = entity.Id,
            Value = serializedOrder,
            Timestamp = Timestamp.Default,
            Headers = new Headers()
            {
                _cinemaNameHeader,
                _orderTypeHeader,
            },
        };

        return _producer.ProduceAsync(KafkaConsts.TopicNames.CinemaOrders, message);
    }

    public void Dispose()
    {
        _producer.Dispose();
    }
}