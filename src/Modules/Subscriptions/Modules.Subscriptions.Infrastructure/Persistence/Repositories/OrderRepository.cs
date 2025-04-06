using CodeUp.SharedKernel.Repositories;
using Modules.Subscriptions.Domain.Entities;
using Modules.Subscriptions.Domain.Repositories;

namespace Modules.Subscriptions.Infrastructure.Persistence.Repositories;

public sealed class OrderRepository(SubscriptionsDbContext context) : IOrderRepository
{
    public void Dispose()
    {
        context.Dispose();
        GC.SuppressFinalize(this);
    }
}