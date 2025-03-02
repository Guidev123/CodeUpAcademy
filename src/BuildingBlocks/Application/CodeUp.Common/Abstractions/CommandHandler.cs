using CodeUp.Common.Notifications;
using CodeUp.Common.Responses;
using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace CodeUp.Common.Abstractions;

public abstract class CommandHandler<TCommand, TResult>(INotificator notificator) : ICommandHandler<TCommand, TResult>
                      where TCommand : IRequest<Response<TResult>>

{
    private readonly INotificator _notificator = notificator;
    public abstract Task<Response<TResult>> Handle(TCommand request, CancellationToken cancellationToken);

    protected void Notify(ValidationResult validationResult)
    {
        foreach (var item in validationResult.Errors) Notify(item.ErrorMessage);
    }

    protected List<string> GetNotifications() => _notificator.GetNotifications().Select(x => x.Message).ToList();

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