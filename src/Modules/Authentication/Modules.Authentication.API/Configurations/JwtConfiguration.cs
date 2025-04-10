using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Modules.Authentication.API.Extensions;

namespace Modules.Authentication.API.Configurations
{
    public static class JwtConfiguration
    {
        public static void AddSecurity(this WebApplicationBuilder builder)
        {
            var appSettingsSection = builder.Configuration.GetSection(nameof(KeycloakExtension));
            if (!appSettingsSection.Exists())
            {
                throw new InvalidOperationException();
            }

            var appSettings = appSettingsSection.Get<KeycloakExtension>();
            if (string.IsNullOrEmpty(appSettings?.AuthorizationUrl))
            {
                throw new InvalidOperationException();
            }

            builder.Services.Configure<KeycloakExtension>(appSettingsSection);

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(o =>
            {
                o.RequireHttpsMetadata = false;
                o.Authority = appSettings.Issuer;
                o.Audience = appSettings.Audience;
                o.MetadataAddress = appSettings.MetadataAddress;
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidAudience = appSettings.Audience,
                    ValidIssuer = appSettings.Issuer,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                };
            });
            builder.Services.AddAuthorization();
        }

        public static void UseSecurity(this WebApplication app)
        {
            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();
        }
    }
}
