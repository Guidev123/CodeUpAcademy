
namespace CodeUp.Common.Abstractions;

public abstract class CommandBase : ICommand
{
    public Guid Id => Guid.NewGuid();
}

public abstract class CommandBase<TResult> : ICommand<TResult>
{
    public Guid Id => Guid.NewGuid();
}
