using CodeUp.Common.Abstractions;
using CodeUp.Common.Notifications;
using CodeUp.Common.Responses;
using Modules.Subscriptions.Domain.Repositories;

namespace Modules.Subscriptions.Application.Commands.Orders.Create
{
    public sealed class CreateOrderHandler(INotificator notificator,
                                           IOrderRepository orderRepository)
                                         : CommandHandler<CreateOrderCommand, CreateOrderResponse>(notificator)
    {
        private readonly IOrderRepository _orderRepository = orderRepository;   

        public override Task<Response<CreateOrderResponse>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
