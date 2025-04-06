using Modules.Students.Domain.Repositories;

namespace Modules.Students.Infrastructure.Persistence.Repositories;

public sealed class StudentRepository(StudentDbContext context) : IStudentRepository
{
    public void Dispose()
    {
        context.Dispose();
        GC.SuppressFinalize(this);
    }
}