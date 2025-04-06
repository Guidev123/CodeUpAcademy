using CodeUp.Common.Abstractions.Mediator;
using CodeUp.Common.Notifications;
using CodeUp.Common.Responses;

namespace CodeUp.Common.Abstractions.Queries;

public abstract class QueryHandler<TQuery, TResult>(INotificator notificator) : Handler(notificator), IRequestHandler<TQuery, Response<TResult>>
    where TQuery : IRequest<Response<TResult>>
{
    public abstract Task<Response<TResult>> ExecuteAsync(TQuery request, CancellationToken cancellationToken);
}
