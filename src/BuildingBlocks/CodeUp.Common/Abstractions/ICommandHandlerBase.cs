using CodeUp.Common.Responses;
using MediatR;

namespace CodeUp.Common.Abstractions;

public interface ICommandHandlerBase<in TCommand> : IRequestHandler<TCommand>
    where TCommand : ICommand
{

}

public interface ICommandHandlerBase<in TCommand, TResult> : IRequestHandler<TCommand, Response<TResult>>
    where TCommand : IRequest<Response<TResult>>
{

}
