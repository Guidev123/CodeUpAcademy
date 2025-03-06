using CodeUp.SharedKernel.DomainObjects;
using Modules.Subscriptions.Domain.Enums;

namespace Modules.Subscriptions.Domain.Entities;

public class Order : Entity, IAggregateRoot
{
    public Order(Guid studentId, Guid voucherId, Guid subscriptionId, bool voucherIsUsed, decimal discount, decimal price)
    {
        StudentId = studentId;
        VoucherId = voucherId;
        SubscriptionId = subscriptionId;
        Code = Guid.NewGuid().ToString("N");
        VoucherIsUsed = voucherIsUsed;
        Discount = discount;
        Price = price;
        Status = OrderStatusEnum.Created;
    }
    protected Order() { }

    public Guid StudentId { get; private set; }
    public Guid VoucherId { get; private set; }
    public Guid SubscriptionId { get; private set; }
    public string Code { get; private set; } = string.Empty;
    public string? ExternalReference { get; private set; }
    public bool VoucherIsUsed { get; private set; }
    public decimal Discount { get; private set; }
    public decimal Price { get; private set; }
    public OrderStatusEnum Status { get; private set; }
    public Subscription? Subscription { get; private set; } 
    public Voucher? Voucher { get; private set; }

    public void SetSubscription(Subscription subscription)
    {
        Subscription = subscription;
    }

    public void SetExternalReference(string extenalReference)
    {
        ExternalReference = extenalReference;
    }
}