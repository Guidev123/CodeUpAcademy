using CodeUp.Common.Extensions;
using CodeUp.Common.Notifications;
using CodeUp.Email;
using CodeUp.Email.Models;
using CodeUp.MessageBus;
using SendGrid.Extensions.DependencyInjection;
using System.Reflection;

namespace CodeUp.API.Configurations;

public static class ApiConfig
{
    public static void AddCommonConfig(this WebApplicationBuilder builder)
    {
        builder.Services.AddSwaggerConfig();
        builder.AddModelsSettings();
        builder.AddEmailServices();
        builder.AddNotifications();
        builder.AddHandlers();
        builder.AddMessageBusConfiguration();
    }

    public static void AddHandlers(this WebApplicationBuilder builder)
    {
        var assemblyFiles = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "Modules.*.dll");

        var assemblies = assemblyFiles.Select(Assembly.LoadFrom).ToArray();
        builder.Services.AddMediatR(opt => opt.RegisterServicesFromAssemblies(assemblies));
    }

    public static void AddModelsSettings(this WebApplicationBuilder builder)
    {
        builder.Services.Configure<JwtExtension>(builder.Configuration.GetSection(nameof(JwtExtension)));
        builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection(nameof(EmailSettings)));
    }

    public static void AddNotifications(this WebApplicationBuilder builder) =>
        builder.Services.AddScoped<INotificator, Notificator>();

    public static void AddEmailServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddSendGrid(x =>
        {
            x.ApiKey = builder.Configuration.GetValue<string>("EmailSettings:ApiKey");
        });
        builder.Services.AddScoped<IEmailService, EmailService>();
    }

    public static void AddMessageBusConfiguration(this WebApplicationBuilder builder) =>
        builder.Services.AddMessageBus(builder.Configuration.GetMessageQueueConnection("MessageBus"));
}
