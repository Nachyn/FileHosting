using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EventBus.Kafka;

public static class Dependencies
{
    public static IServiceCollection AddKafkaEventBus(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions();
        services.AddSingleton(configuration.GetSection("KafkaEventBusSettings").Get<KafkaEventBusSettings>()!);
        services.AddSingleton<IEventBus, KafkaEventBus>();

        return services;
    }
}