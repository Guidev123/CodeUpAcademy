using CodeUp.Common.Notifications;
using CodeUp.Common.Responses;
using MediatR;

namespace CodeUp.Common.Abstractions;

public abstract class CommandHandler<TCommand, TResult>(INotificator notificator) : Handler(notificator), IRequestHandler<TCommand, Response<TResult>>
                      where TCommand : IRequest<Response<TResult>>

{
    public abstract Task<Response<TResult>> Handle(TCommand request, CancellationToken cancellationToken);
}