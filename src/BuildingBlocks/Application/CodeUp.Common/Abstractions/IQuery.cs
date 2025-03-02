using CodeUp.Common.Responses;
using MediatR;

namespace CodeUp.Common.Abstractions;

public interface IQuery<TResult> : IRequest<Response<TResult>>
{
}

public interface IPagedQuery<TResult> : IRequest<PagedResponse<TResult>>
{
}