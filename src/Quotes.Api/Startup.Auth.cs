using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Quotes.Domain.Settings;
using System;
using System.Text;

namespace Quotes.Api
{
    public partial class Startup
    {
        private SymmetricSecurityKey _signingKey;

        private TokenValidationParameters _tokenValidationParameters;

        private TokenProviderSettings _tokenProviderOptions;

        private void ConfigureAuth(IServiceCollection services)
        {
            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options => { options.TokenValidationParameters = _tokenValidationParameters; });
        }

        private void ConfigureTokens()
        {
            var tokenProviderSettings = Configuration.GetSection("App").Get<AppSettings>().TokenProviderSettings;

            _signingKey =
                new SymmetricSecurityKey(
                    Encoding.ASCII.GetBytes(tokenProviderSettings.SecretKey));

            _tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = _signingKey,
                ValidateIssuer = true,
                ValidIssuer = tokenProviderSettings.Issuer,
                ValidateAudience = true,
                ValidAudience = tokenProviderSettings.Audience,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            _tokenProviderOptions = new TokenProviderSettings
            {
                TokenPath = tokenProviderSettings.TokenPath,
                Issuer = tokenProviderSettings.Issuer,
                Audience = tokenProviderSettings.Audience,
                SigningCredentials = new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256),
                ExpirationInMinutes = tokenProviderSettings.ExpirationInMinutes
            };
        }
    }
}