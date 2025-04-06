namespace CodeUp.Common.Abstractions.Mediator
{
    public interface IMediator
    {
        Task<TResponse> DispatchAsync<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default);
    }
}
