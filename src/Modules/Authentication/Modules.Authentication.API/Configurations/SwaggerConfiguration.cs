using Microsoft.OpenApi.Models;
using Modules.Authentication.API.Extensions;

namespace Modules.Authentication.API.Configurations
{
    public static class SwaggerConfiguration
    {
        public static void AddSwagger(this WebApplicationBuilder builder)
        {
            var appSettingsSection = builder.Configuration.GetSection(nameof(KeycloakExtension));
            var appSettings = appSettingsSection.Get<KeycloakExtension>()
                ?? throw new InvalidOperationException("Keycloak settings not found.");

            builder.Services.Configure<KeycloakExtension>(appSettingsSection);

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.CustomSchemaIds(id => id.FullName!.Replace('+', '-'));

                c.AddSecurityDefinition("Keycloak", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows
                    {
                        AuthorizationCode = new OpenApiOAuthFlow
                        {
                            AuthorizationUrl = new Uri(appSettings.AuthorizationUrl),
                            TokenUrl = new Uri(appSettings.TokenUrl),
                            Scopes = new Dictionary<string, string>
                            {
                                { "openid", "OpenID Connect" },
                                { "profile", "Profile" }
                            }
                        }
                    }
                });

                var securityRequirement = new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Keycloak"
                            },
                            In = ParameterLocation.Header,
                            Name = "Bearer",
                            Scheme = "Bearer"
                        },
                        Array.Empty<string>()
                    }
                };
                c.AddSecurityRequirement(securityRequirement);
            });
        }

        public static void UseCustomSwagger(this WebApplication app, WebApplicationBuilder builder)
        {
            var appSettingsSection = builder.Configuration.GetSection(nameof(KeycloakExtension));
            var appSettings = appSettingsSection.Get<KeycloakExtension>()
                ?? throw new InvalidOperationException("Keycloak settings not found.");

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
                c.OAuthClientId(appSettings.ClientId);
                c.OAuthUsePkce();
                c.OAuthScopeSeparator(" ");
            });
        }
    }
}