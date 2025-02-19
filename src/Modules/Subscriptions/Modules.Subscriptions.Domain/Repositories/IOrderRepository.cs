using Modules.Subscriptions.Domain.Entities;

namespace Modules.Subscriptions.Domain.Repositories;

public interface IOrderRepository
{
    Task CreateAsync(Order order);
    Task<List<Order>> GetAllByStudentId(Guid studentId);
    void Update(Order order);   
}
