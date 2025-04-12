using CodeUp.SharedKernel.DomainObjects;
using Modules.Subscriptions.Domain.Enums;

namespace Modules.Subscriptions.Domain.Entities;

public class Order : Entity, IAggregateRoot
{
    public Order(Guid studentId, bool voucherIsUsed, decimal discount, decimal price)
    {
        StudentId = studentId;
        Code = Guid.NewGuid().ToString("N");
        VoucherIsUsed = voucherIsUsed;
        Discount = discount;
        Price = price;
        Status = OrderStatusEnum.Created;
        Validate();
    }

    protected Order()
    { }

    public Guid StudentId { get; private set; }
    public Guid? VoucherId { get; private set; }
    public Guid? SubscriptionId { get; private set; }
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
        AssertionConcern.EnsureNotNull(subscription, "Subscription must be not empty.");
        AssertionConcern.EnsureDifferent(subscription.Id, Guid.Empty, "Subscription Id must be not empty.");
        Subscription = subscription;
        SubscriptionId = subscription.Id;
    }

    public void SetVoucher(Voucher voucher)
    {
        AssertionConcern.EnsureNotNull(voucher, "Voucher must be not empty.");
        AssertionConcern.EnsureDifferent(voucher.Id, Guid.Empty, "Voucher Id must be not empty.");
        Voucher = voucher;
        VoucherId = voucher.Id;
    }

    public void SetExternalReference(string extenalReference)
    {
        AssertionConcern.EnsureNotEmpty(extenalReference, "External Reference must be not empty.");
        ExternalReference = extenalReference;
    }

    public override void Validate()
    {
        AssertionConcern.EnsureDifferent(StudentId, Guid.Empty, "Student Id must be not empty.");
        AssertionConcern.EnsureNotEmpty(Code, "Order Code must be not empty.");
        AssertionConcern.EnsureGreaterThan(Price, 0, "Price must be greater than or equal to zero.");
        AssertionConcern.EnsureGreaterThan(Discount, 0, "Discount must be greater than or equal to zero.");
        AssertionConcern.EnsureTrue(Status == OrderStatusEnum.Created, "Order status must be 'Created'.");

        if (VoucherIsUsed && VoucherId.HasValue)
            AssertionConcern.EnsureNotNull(VoucherId, "Voucher must be assigned if used.");

        if (SubscriptionId.HasValue && Subscription is not null)
            AssertionConcern.EnsureDifferent(SubscriptionId.Value, Guid.Empty, "Subscription Id must be not empty if assigned.");

        if (!Enum.IsDefined(Status))
            throw new DomainException("Invalid order status.");
    }
}