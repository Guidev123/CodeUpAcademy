using CodeUp.SharedKernel.ValueObjects;

namespace Modules.Subscriptions.Domain.ValueObjects
{
    public record SubscriptionBenefit(string Name, string Description) : ValueObject;
}
