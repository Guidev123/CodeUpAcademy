using CodeUp.SharedKernel.DomainObjects;

namespace CodeUp.SharedKernel.Repositories
{
    public interface IRepository<TE> : IDisposable
        where TE : IAggregateRoot
    {
        // Marker interface for repositories
    }
}
