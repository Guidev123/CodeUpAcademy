using CodeUp.Common.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Modules.Authentication.Application.Services;
using Modules.Authentication.Domain.Repositories;
using Modules.Authentication.Infrastructure.Persistence;
using Modules.Authentication.Infrastructure.Persistence.Repositories;
using Modules.Authentication.Infrastructure.Services;

namespace Modules.Authentication.Infrastructure;

public static class InfrastructureModule
{
    public static void AddAutheticationModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHandlers();
        services.AddModelSettings(configuration);
        services.AddRepositories();
        services.AddTokenService();
        services.AddPasswordHasherService();
        services.AddDbContext(configuration);
    }

    public static void AddRepositories(this IServiceCollection services) =>
        services.AddScoped<IUserRepository, UserRepository>();

    public static void AddTokenService(this IServiceCollection services) =>
        services.AddTransient<ITokenService, TokenService>();

    public static void AddPasswordHasherService(this IServiceCollection services)
        => services.AddTransient<IPasswordHasherService, PasswordHashService>();

    public static void AddHandlers(this IServiceCollection services)
        => services.AddMediatR(opt => opt.RegisterServicesFromAssembly(typeof(InfrastructureModule).Assembly));

    public static void AddModelSettings(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JwtExtension>(options => configuration.GetSection(nameof(JwtExtension)));
    }

    public static void AddDbContext(this IServiceCollection services, IConfiguration configuration) =>
        services.AddDbContext<AuthenticationDbContext>(opt =>
        opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection") ?? string.Empty));
}
