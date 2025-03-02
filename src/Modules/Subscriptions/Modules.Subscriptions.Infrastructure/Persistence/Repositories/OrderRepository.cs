using Modules.Subscriptions.Domain.Entities;
using Modules.Subscriptions.Domain.Repositories;

namespace Modules.Subscriptions.Infrastructure.Persistence.Repositories;

public sealed class OrderRepository(IRepository<Order> repository) : IOrderRepository
{
    private readonly IRepository<Order> _repository = repository;

    public async Task CreateAsync(Order order) => await _repository.CreateAsync(order);

    public async Task<List<Order>> GetAllByStudentId(Guid studentId) => await _repository.GetAllAsync(x => x.StudentId == studentId);

    public void Update(Order order) => _repository.Update(order);
}