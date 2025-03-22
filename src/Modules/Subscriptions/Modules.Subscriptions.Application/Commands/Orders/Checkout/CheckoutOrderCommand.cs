using CodeUp.Common.Abstractions;

namespace Modules.Subscriptions.Application.Commands.Orders.Checkout
{
    public record CheckoutOrderCommand() : Command<CheckoutOrderResponse>;
}
