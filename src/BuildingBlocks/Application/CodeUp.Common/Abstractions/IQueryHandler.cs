using CodeUp.Common.Responses;
using MediatR;

namespace CodeUp.Common.Abstractions;

public interface IQueryHandler<TQuery, TResult> :
    IRequestHandler<TQuery, Response<TResult>>
    where TQuery : IRequest<Response<TResult>>
{

}
