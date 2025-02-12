using Modules.Students.Domain.Entities;

namespace Modules.Students.Domain.Repositories;

public interface IStudentRepository : IDisposable
{
    Task CreateAsync(Student student);
    Task<bool> AlreadyExistsAsync(string document);
    void Update(Student student);
    Task DeleteAsync(Guid id);  
    Task<Address?> GetAddressAsync(Guid studentId);
    Task<bool> SaveChangesAsync();
}
