using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Modules.Subscriptions.Domain.Repositories;
using Modules.Subscriptions.Infrastructure.Persistence.Repositories;

namespace Modules.Subscriptions.Infrastructure;

public static class InfrastructureModule
{
    public static void AddSubscriptionModule(this IServiceCollection services, IConfiguration configuration)
    {
    }

    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IOrderRepository, OrderRepository>();
    }
}