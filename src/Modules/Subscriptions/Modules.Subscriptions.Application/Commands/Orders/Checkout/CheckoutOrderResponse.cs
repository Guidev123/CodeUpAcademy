namespace Modules.Subscriptions.Application.Commands.Orders.Checkout
{
    public record CheckoutOrderResponse(Guid OrderId, string Session);
}
