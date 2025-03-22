using FluentValidation;

namespace Modules.Subscriptions.Application.Commands.Orders.Checkout
{
    public sealed class CheckoutOrderValidation : AbstractValidator<CheckoutOrderCommand>   
    {
        public CheckoutOrderValidation()
        {
            
        }
    }
}
