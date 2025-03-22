using CodeUp.Common.Abstractions;

namespace Modules.Subscriptions.Application.Commands.Orders.Create
{
    public record CreateOrderCommand() : Command<CreateOrderResponse>;
}
