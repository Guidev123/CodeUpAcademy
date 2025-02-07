using CodeUp.Common.Extensions;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace CodeUp.API.Configurations;

public static class ApiConfig
{
    public static void AddHandlers(this WebApplicationBuilder builder)
    {
        var assemblyFiles = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "Modules.*.dll");

        var assemblies = assemblyFiles.Select(Assembly.LoadFrom).ToArray();
        builder.Services.AddMediatR(opt => opt.RegisterServicesFromAssemblies(assemblies));
    }

    public static void AddModelsSettings(this WebApplicationBuilder builder)
    {
        builder.Services.Configure<JwtExtension>(builder.Configuration.GetSection(nameof(JwtExtension)));
    }

    public static void AddSwaggerConfig(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo()
            {
                Title = "API CodeUp",
                Contact = new OpenApiContact() { Name = "Guilherme Nascimento", Email = "guirafaelrn@gmail.com" },
                License = new OpenApiLicense() { Name = "MIT", Url = new Uri("https://opensource.org/license/MIT") }
            });

            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "Enter the JWT token in this format: Bearer {your token}",
                Name = "Authorization",
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });
    }


    public static void UseSwaggerConfig(this IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1"));
    }
}
