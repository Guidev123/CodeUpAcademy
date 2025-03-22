using FluentValidation;

namespace Modules.Subscriptions.Application.Commands.Orders.Create
{
    public sealed class CreateOrderValidation : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderValidation()
        {
            
        }
    }
}
