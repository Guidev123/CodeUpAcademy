using CodeUp.Common.Notifications;
using CodeUp.Common.Responses;
using FluentValidation.Results;
using MediatR;

namespace CodeUp.Common.Abstractions;

public abstract class QueryHandler<TQuery, TResult>(INotificator notificator) : IQueryHandler<TQuery, TResult>, IRequestHandler<TQuery, Response<TResult>>
    where TQuery : IRequest<Response<TResult>>
{
    public abstract Task<Response<TResult>> Handle(TQuery request, CancellationToken cancellationToken);

    private readonly INotificator _notificator = notificator;
    protected void Notify(ValidationResult validationResult)
    {
        foreach (var item in validationResult.Errors) Notify(item.ErrorMessage);
    }

    protected List<string> GetNotifications() => _notificator.GetNotifications().Select(x => x.Message).ToList();

    protected void Notify(string message) => _notificator.HandleNotification(new(message));
}
