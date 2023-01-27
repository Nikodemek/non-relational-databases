using System.Linq;
using Parsevoir;

namespace Cinema.Consumer.Kafka;

public static class ArgsParser
{
    public static KafkaConfig ParseKafkaConfig(string[] arguments)
    {
        string? bootstrapServers = GetValue(nameof(KafkaConfig.BootstrapServers));
        string? groupId = GetValue(nameof(KafkaConfig.GroupId));

        KafkaConfig config = new();
        if (bootstrapServers is not null) config.BootstrapServers = bootstrapServers;
        if (groupId is not null) config.GroupId = groupId;
        
        return config;

        
        string? GetValue(string argName)
        {
            string template = $"{argName}={{0}}";
            string? value = arguments.SingleOrDefault(s => s.StartsWith(argName));

            return value is not null
                ? Parse.Single<string>(value, template)
                : null;
        }
    }
}