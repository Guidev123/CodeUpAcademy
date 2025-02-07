﻿using CodeUp.Common.Extensions;
using CodeUp.Common.Notifications;
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
        services.AddNotifications();
        services.AddRepositories();
        services.AddTokenService();
        services.AddPasswordHasherService();
        services.AddDbContext(configuration);
    }

    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }

    public static void AddNotifications(this IServiceCollection services)
        => services.AddScoped<INotificator, Notificator>();

    public static void AddTokenService(this IServiceCollection services) =>
        services.AddTransient<ITokenService, TokenService>();

    public static void AddPasswordHasherService(this IServiceCollection services)
        => services.AddTransient<IPasswordHasherService, PasswordHashService>();

    public static void AddDbContext(this IServiceCollection services, IConfiguration configuration) =>
        services.AddDbContext<AuthenticationDbContext>(opt =>
        opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection") ?? string.Empty));
}
