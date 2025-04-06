using CodeUp.Common.Abstractions.Mediator;
using CodeUp.Common.Responses;

namespace CodeUp.Common.Abstractions.Queries;

public interface IQuery<TResult> : IRequest<Response<TResult>>
{
}
