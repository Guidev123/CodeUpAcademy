namespace Modules.Subscriptions.Application.Commands.Orders.Create
{
    public record CreateOrderResponse(Guid OrderId, DateTime CreatedAt)
    {
        public CreateOrderResponse(Guid OrderId) : this(OrderId, DateTime.Now) { }
    }
}
