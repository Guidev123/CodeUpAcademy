using CodeUp.Common.Abstractions;
using CodeUp.Common.Notifications;
using CodeUp.Common.Responses;
using Modules.Subscriptions.Domain.Repositories;

namespace Modules.Subscriptions.Application.Commands.Orders.Checkout
{
    public sealed class CheckoutOrderHandler(INotificator notificator,
                                             IOrderRepository orderRepository) 
                                           : CommandHandler<CheckoutOrderCommand, CheckoutOrderResponse>(notificator)
    {
        private readonly IOrderRepository _orderRepository = orderRepository;   

        public override Task<Response<CheckoutOrderResponse>> Handle(CheckoutOrderCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
