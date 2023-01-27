using System.Text.Json;
using Cinema.Consumer.Kafka.Interfaces;
using Cinema.Entity.Interfaces;
using Cinema.Entity.Kafka;
using Cinema.Repository.Interfaces;
using Confluent.Kafka;

namespace Cinema.Consumer.Kafka;

internal class KafkaConsumer<TEntity> : IKafkaConsumer<TEntity>
    where TEntity : class, IMongoEntity<TEntity>
{
    private readonly ICommonsRepository<TEntity> _repository;
    private readonly IConsumer<string, string> _consumer;

    public KafkaConsumer(
        KafkaConfig kafkaConfig,
        ICommonsRepository<TEntity> repository,
        string topic)
    {
        _repository = repository;
        var config = new ConsumerConfig()
        {
            GroupId = kafkaConfig.GroupId,
            BootstrapServers = kafkaConfig.BootstrapServers,
            AutoOffsetReset = kafkaConfig.AutoOffsetReset,
        };

        var consumerBuilder = new ConsumerBuilder<string, string>(config);

        _consumer = consumerBuilder.Build();
        _consumer.Subscribe(topic);
    }

    public async Task<FullResponse<string, string, TEntity>> ConsumeSingleAsync(CancellationToken cancellationToken)
    {
        var consumeResult = _consumer.Consume(cancellationToken);
        TEntity? entity = null;

        if (consumeResult?.Message is Message<string, string> message)
        {
            var typeHeader = message.Headers.Single(h => String.Equals(h.Key, "Type"));
            string typeFullName = KafkaConsts.Encoding.GetString(typeHeader.GetValueBytes());

            if (typeof(TEntity).FullName != typeFullName)
                throw new Exception("Received type does not match target type!");

            entity = JsonSerializer.Deserialize<TEntity>(message.Value)
                ?? throw new JsonException("Could not deserialize Order entity!");
            
            await _repository.CreateAsync(entity);
        }

        FullResponse<string, string, TEntity> fullResponse = new()
        {
            Success = consumeResult is not null && entity is not null,
            Entity = entity,
            ConsumeResult = consumeResult,
        };

        return fullResponse;
    }

    public string GetMemberId() => _consumer.MemberId;

    public string GetName() => _consumer.Name;

    public void Dispose()
    {
        _consumer.Dispose();
    }
}