using CodeUp.Common.Abstractions.Mediator;
using CodeUp.Common.Responses;

namespace CodeUp.Common.Abstractions;

public interface ICommand<TResult> : IRequest<Response<TResult>>
{
    Guid Id { get; }
}