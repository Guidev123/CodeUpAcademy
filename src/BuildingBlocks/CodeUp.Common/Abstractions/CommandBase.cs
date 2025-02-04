
namespace CodeUp.Common.Abstractions;

public abstract record CommandBase : ICommand
{
    public Guid Id => Guid.NewGuid();
}

public abstract record CommandBase<TResult> : ICommand<TResult>
{
    public Guid Id => Guid.NewGuid();
}
