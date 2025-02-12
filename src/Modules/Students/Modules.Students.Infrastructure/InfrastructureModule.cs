using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Modules.Students.Domain.Repositories;
using Modules.Students.Infrastructure.BackgroundServices;
using Modules.Students.Infrastructure.Persistence;
using Modules.Students.Infrastructure.Persistence.Repositories;

namespace Modules.Students.Infrastructure;

public static class InfrastructureModule
{
    public static void AddStudentModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext(configuration);
        services.AddRepositories();
        services.AddBackgroundServices();
    }

    public static void AddBackgroundServices(this IServiceCollection services)
        => services.AddHostedService<UserCreatedConsumer>();

    public static void AddRepositories(this IServiceCollection services)
        => services.AddScoped<IStudentRepository, StudentRepository>();

    public static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
        => services.AddDbContext<StudentDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection") ?? string.Empty)
                   .LogTo(Console.WriteLine)
                   .EnableDetailedErrors());
}
