using CodeUp.Common.Abstractions.Mediator;
using CodeUp.Common.Notifications;
using CodeUp.Common.Responses;

namespace CodeUp.Common.Abstractions.Queries;

public abstract class PagedQueryHandler<TQuery, TResult>(INotificator notificator) : Handler(notificator), IRequestHandler<TQuery, PagedResponse<TResult>>
    where TQuery : IRequest<PagedResponse<TResult>>
{
    public abstract Task<PagedResponse<TResult>> ExecuteAsync(TQuery request, CancellationToken cancellationToken);
}