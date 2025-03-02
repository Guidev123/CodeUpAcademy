using Microsoft.EntityFrameworkCore;
using Modules.Students.Domain.Entities;
using Modules.Students.Domain.Repositories;

namespace Modules.Students.Infrastructure.Persistence.Repositories;

public sealed class StudentRepository(StudentDbContext context) : IStudentRepository
{
    private readonly StudentDbContext _context = context;

    public async Task<Student?> GetByIdAsync(Guid id)
        => await _context.Students.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

    public async Task CreateAsync(Student student)
        => await _context.Students.AddAsync(student);

    public async Task DeleteAsync(Guid id)
        => await _context.Database.ExecuteSqlInterpolatedAsync(@$"
            UPDATE students.Students
                SET IsDeleted = 1,
                DeletedAt = GETDATE()
            WHERE Id = {id}");

    public async Task<Address?> GetAddressAsync(Guid studentId)
        => await _context.Address.AsNoTracking().FirstOrDefaultAsync(x => x.StudentId == studentId);

    public void Update(Student student)
        => _context.Students.Update(student);

    public async Task<bool> AlreadyExistsAsync(string document)
        => await _context.Students.AnyAsync(x => x.Document.Number == document);

    public async Task<bool> SaveChangesAsync()
        => await _context.SaveChangesAsync() > 0;

    public void Dispose()
    {
        _context.Dispose();
        GC.SuppressFinalize(this);
    }
}