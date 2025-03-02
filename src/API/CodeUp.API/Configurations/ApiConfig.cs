using CodeUp.Common.Extensions;
using CodeUp.Common.Notifications;
using CodeUp.Email;
using CodeUp.Email.Models;
using CodeUp.MessageBus;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using SendGrid.Extensions.DependencyInjection;
using System.Reflection;
using System.Text;

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

    public static void AddCorsConfig(this WebApplicationBuilder builder)
    {
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("Total", policy =>
            {
                policy.AllowAnyOrigin()
                      .AllowAnyHeader()
                      .AllowAnyMethod();
            });
        });
    }

    public static void AddSecurity(this WebApplicationBuilder builder)
    {
        var appSettingsSection = builder.Configuration.GetSection(nameof(JwtExtension));
        builder.Services.Configure<JwtExtension>(appSettingsSection);

        var appSettings = appSettingsSection.Get<JwtExtension>() ?? new();
        var key = Encoding.ASCII.GetBytes(appSettings.Secret);

        builder.Services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(x =>
        {
            x.RequireHttpsMetadata = true;
            x.SaveToken = true;
            x.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidAudience = appSettings.Audience,
                ValidIssuer = appSettings.Issuer
            };
        });
        builder.Services.AddAuthorization();
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

    public static void UseSecurity(this IApplicationBuilder app)
    {
        app.UseSwaggerConfig();

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseCors("Total");

        app.UseAuthentication();
        app.UseAuthorization();
    }
}