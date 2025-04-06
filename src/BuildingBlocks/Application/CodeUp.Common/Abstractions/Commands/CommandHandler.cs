using CodeUp.Common.Abstractions.Mediator;
using CodeUp.Common.Notifications;
using CodeUp.Common.Responses;

namespace CodeUp.Common.Abstractions.Commands;

public abstract class CommandHandler<TCommand, TResult>(INotificator notificator) : Handler(notificator), IRequestHandler<TCommand, Response<TResult>>
                      where TCommand : IRequest<Response<TResult>>

{
    public abstract Task<Response<TResult>> ExecuteAsync(TCommand request, CancellationToken cancellationToken);
}