using CodeUp.Common.Notifications;
using CodeUp.Common.Responses;
using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace CodeUp.Common.Abstractions;

public abstract class CommandHandlerBase<TCommand>
                    : ICommandHandlerBase<TCommand>
                      where TCommand : ICommand
{
    public abstract Task Handle(TCommand request, CancellationToken cancellationToken);
}

public abstract class CommandHandlerBase<TCommand, TResult>(INotificator notificator) : IRequestHandler<TCommand, Response<TResult>>
                      where TCommand : IRequest<Response<TResult>>

{
    private readonly INotificator _notificator = notificator;
    public abstract Task<Response<TResult>> Handle(TCommand request, CancellationToken cancellationToken);

    protected void Notify(ValidationResult validationResult)
    {
        foreach (var item in validationResult.Errors) Notify(item.ErrorMessage);

    }

    protected void Notify(string message) => _notificator.HandleNotification(new(message));

    protected bool ExecuteValidation<TV, TC>(TV validation, TC command)
                   where TV : AbstractValidator<TC>
                   where TC : class
    {
        var validator = validation.Validate(command);

        if (validator.IsValid) return true;

        Notify(validator);

        return false;
    }
}