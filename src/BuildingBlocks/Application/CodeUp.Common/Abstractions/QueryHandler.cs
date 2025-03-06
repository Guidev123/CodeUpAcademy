using CodeUp.Common.Notifications;
using CodeUp.Common.Responses;
using MediatR;

namespace CodeUp.Common.Abstractions;

public abstract class QueryHandler<TQuery, TResult>(INotificator notificator) : Handler(notificator), IRequestHandler<TQuery, Response<TResult>>
    where TQuery : IRequest<Response<TResult>>
{
    public abstract Task<Response<TResult>> Handle(TQuery request, CancellationToken cancellationToken);
}


public abstract class PagedQueryHandler<TQuery, TResult>(INotificator notificator) : Handler(notificator), IRequestHandler<TQuery, PagedResponse<TResult>>
    where TQuery : IRequest<PagedResponse<TResult>>
{
    public abstract Task<PagedResponse<TResult>> Handle(TQuery request, CancellationToken cancellationToken);
}