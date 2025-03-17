using CodeUp.SharedKernel.DomainObjects;
using Modules.Subscriptions.Domain.Enums;

namespace Modules.Subscriptions.Domain.Entities;

public class Voucher : Entity
{
    public Voucher(string code, decimal? discountValue, int? discountPercentage, int maxUsage, int currentUsage, VoucherTypeEnum type, DateTime expirationDate)
    {
        Code = code;
        DiscountValue = discountValue;
        DiscountPercentage = discountPercentage;
        MaxUsage = maxUsage;
        CurrentUsage = currentUsage;
        Type = type;
        ExpirationDate = expirationDate;
        Validate();
    }

    public string Code { get; private set; } = string.Empty;
    public decimal? DiscountValue { get; private set; }
    public int? DiscountPercentage { get; private set; }
    public int MaxUsage { get; private set; }
    public int CurrentUsage { get; private set; }
    public VoucherTypeEnum Type { get; private set; }
    public DateTime ExpirationDate { get; private set; }
    public bool IsActive => DateTime.Now <= ExpirationDate && CurrentUsage < MaxUsage;

    public override void Validate()
    {
        AssertionConcern.EnsureNotEmpty(Code, "Voucher code must be not empty.");
        AssertionConcern.EnsureGreaterThan(MaxUsage, 1, "Max usage must be at least 1.");
        AssertionConcern.EnsureGreaterThan(CurrentUsage, 0, "Current usage cannot be negative.");
        AssertionConcern.EnsureGreaterThan(CurrentUsage, MaxUsage, "Current usage cannot exceed max usage.");
        AssertionConcern.EnsureTrue(ExpirationDate <= DateTime.Now, "Expiration date must be in the future.");

        if (DiscountValue.HasValue)
            AssertionConcern.EnsureGreaterThan(DiscountValue.Value, 0, "Discount value must be greater than or equal to zero.");

        if (DiscountPercentage.HasValue)
        {
            AssertionConcern.EnsureGreaterThan(DiscountPercentage.Value, 1, "Discount percentage must be at least 1%.");
            AssertionConcern.EnsureTrue(DiscountPercentage.Value <= 100, "Discount percentage cannot exceed 100%.");
        }
    }

}