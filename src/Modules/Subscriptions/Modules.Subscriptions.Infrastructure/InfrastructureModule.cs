using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Modules.Subscriptions.Domain.Repositories;
using Modules.Subscriptions.Infrastructure.Persistence.Repositories;

namespace Modules.Subscriptions.Infrastructure;

public static class InfrastructureModule
{
    public static void AddSubscriptionModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddRepositories();
    }

    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IOrderRepository, OrderRepository>();
    }
}