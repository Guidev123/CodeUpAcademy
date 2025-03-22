using CodeUp.Common.Abstractions;

namespace Modules.Subscriptions.Application.Commands.Orders.ConfirmPayment
{
    public record ConfirmPaymentCommand() : Command<ConfirmPaymentResponse>;
}
