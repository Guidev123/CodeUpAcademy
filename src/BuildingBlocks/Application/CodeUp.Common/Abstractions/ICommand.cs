using CodeUp.Common.Responses;
using MediatR;

namespace CodeUp.Common.Abstractions;

public interface ICommand : IRequest
{
    Guid Id { get; }
}

public interface ICommand<TResult> : IRequest<Response<TResult>>
{
    Guid Id { get; }
}
