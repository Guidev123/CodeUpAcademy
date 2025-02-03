using CodeUp.Common.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Modules.Authentication.Application.Services;
using Modules.Authentication.Domain.Repositories;
using Modules.Authentication.Infrastructure.Persistence.Repositories;
using Modules.Authentication.Infrastructure.Services;

namespace Modules.Authentication.Infrastructure;

public static class InfrastructureModule
{
    public static void AddAutheticationModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddModelSettings(configuration);
        services.AddRepositories();
        services.AddTokenService();
    }

    public static void AddRepositories(this IServiceCollection services) =>
        services.AddScoped<IUserRepository, UserRepository>();

    public static void AddTokenService(this IServiceCollection services) =>
        services.AddTransient<ITokenService, TokenService>();

    public static void AddModelSettings(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JwtExtension>(options => configuration.GetSection(nameof(JwtExtension)));
    }
}
