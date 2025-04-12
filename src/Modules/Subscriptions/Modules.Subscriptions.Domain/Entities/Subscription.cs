using CodeUp.SharedKernel.DomainObjects;
using Modules.Subscriptions.Domain.ValueObjects;

namespace Modules.Subscriptions.Domain.Entities;

public class Subscription : Entity, IAggregateRoot
{
    public Subscription(string name, string description, decimal price, int durationInDays, bool isActive)
    {
        Name = name;
        Description = description;
        Price = price;
        DurationInDays = durationInDays;
        IsActive = isActive;
        ExpiresAt = DateTime.Now.AddDays(durationInDays);
    }

    protected Subscription()
    { }

    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public decimal Price { get; private set; }
    public int DurationInDays { get; private set; }
    public DateTime ExpiresAt { get; private set; }
    public bool IsActive { get; private set; }
    private readonly List<SubscriptionBenefit> _benefits = [];
    public IReadOnlyCollection<SubscriptionBenefit> Benefits => _benefits;

    public override void Validate()
    {
        AssertionConcern.EnsureNotEmpty(Name, "Subscription name must be not empty.");
        AssertionConcern.EnsureNotEmpty(Description, "Subscription description must be not empty.");
        AssertionConcern.EnsureGreaterThan(Price, 0, "Subscription price must be greater than or equal to zero.");
        AssertionConcern.EnsureGreaterThan(DurationInDays, 0, "Subscription duration must be greater than zero.");
        AssertionConcern.EnsureTrue(ExpiresAt.Day == DateTime.Now.AddDays(DurationInDays).Day, "Subscription duration cannot be less than duration in days.");
    }
}