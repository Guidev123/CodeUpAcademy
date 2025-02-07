using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CodeUp.MessageBus;

public static class DependencyInjector
{
    public static void AddMessageBus(this IServiceCollection services, string connection)
    {
        if (string.IsNullOrEmpty(connection)) throw new();

        services.AddSingleton<IMessageBus>(new MessageBus(connection));
    }

    public static string GetMessageQueueConnection(this IConfiguration configuration, string name) =>
        configuration?.GetSection("MessageQueueConnection")?[name] ?? string.Empty;
}
