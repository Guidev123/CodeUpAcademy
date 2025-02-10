
namespace CodeUp.Common.Abstractions;

public abstract record Command<TResult> : ICommand<TResult>
{
    public Guid Id => Guid.NewGuid();
}
