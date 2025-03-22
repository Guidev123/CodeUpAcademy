using CodeUp.Common.Abstractions;
using CodeUp.Common.Notifications;
using CodeUp.Common.Responses;
using Modules.Subscriptions.Domain.Repositories;

namespace Modules.Subscriptions.Application.Commands.Orders.ConfirmPayment
{
    public sealed class ConfirmPaymentHandler(INotificator notificator,
                                              IOrderRepository orderRepository)
                                            : CommandHandler<ConfirmPaymentCommand, ConfirmPaymentResponse>(notificator)
    {
        private readonly IOrderRepository _orderRepository = orderRepository;

        public override Task<Response<ConfirmPaymentResponse>> Handle(ConfirmPaymentCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
