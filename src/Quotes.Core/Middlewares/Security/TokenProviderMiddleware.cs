using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Quotes.Core.Services.Security;
using Quotes.Domain.Settings;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Quotes.Core.Middlewares.Security
{
    public class TokenProviderMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly TokenProviderSettings _options;
        private readonly IIdentityService _identityService;

        public TokenProviderMiddleware(
            RequestDelegate next,
            IOptions<TokenProviderSettings> options,
            IIdentityService identityService
            )
        {
            ThrowIfInvalidOptions(options.Value);
            _next = next;
            _identityService = identityService;
            _options = options.Value;
        }

        public Task Invoke(HttpContext context)
        {
            if (!context.Request.Path.Equals(_options.TokenPath, StringComparison.Ordinal))
            {
                return _next(context);
            }

            if (!context.Request.Method.Equals("POST") || !context.Request.HasFormContentType)
            {
                context.Response.StatusCode = 400;
                return context.Response.WriteAsync("Bad request.");
            }
            return GenerateToken(context);
        }

        private async Task GenerateToken(HttpContext context)
        {
            var username = context.Request.Form["username"];
            var password = context.Request.Form["password"];

            var identity = await _identityService.GetIdentity(username, password);
            if (identity == null)
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync("Invalid username or password.");
                return;
            }
            var now = DateTime.UtcNow;
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim(JwtRegisteredClaimNames.Jti, await _options.NonceGenerator()),
                new Claim(JwtRegisteredClaimNames.Iat,
                    new DateTimeOffset(now).ToUniversalTime().ToUnixTimeSeconds().ToString(),
                    ClaimValueTypes.Integer64)
            };

            var response = GetSerializedResponse(claims, now);

            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(response);
        }

        private string GetSerializedResponse(IEnumerable<Claim> claims, DateTime now)
        {
            var encodedJwt = GetEncodedJwtToken(claims, now);

            var response = new
            {
                access_token = encodedJwt,
                expires_in = now.AddMinutes(_options.ExpirationInMinutes),
            };

            return JsonConvert.SerializeObject(response, new JsonSerializerSettings
            {
                Formatting = Formatting.Indented
            });
        }

        private string GetEncodedJwtToken(IEnumerable<Claim> claims, DateTime now)
        {
            var jwt = new JwtSecurityToken(
                issuer: _options.Issuer,
                audience: _options.Audience,
                claims: claims,
                notBefore: now,
                expires: now.AddMinutes(_options.ExpirationInMinutes),
                signingCredentials: _options.SigningCredentials);
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            return encodedJwt;
        }

        private static void ThrowIfInvalidOptions(TokenProviderSettings options)
        {
            if (string.IsNullOrEmpty(options.TokenPath))
            {
                throw new ArgumentNullException(nameof(TokenProviderSettings.TokenPath));
            }

            if (string.IsNullOrEmpty(options.Issuer))
            {
                throw new ArgumentNullException(nameof(TokenProviderSettings.Issuer));
            }

            if (string.IsNullOrEmpty(options.Audience))
            {
                throw new ArgumentNullException(nameof(TokenProviderSettings.Audience));
            }

            if (options.ExpirationInMinutes == 0)
            {
                throw new ArgumentException("Must be a non-zero TimeSpan.", nameof(TokenProviderSettings.ExpirationInMinutes));
            }

            if (options.SigningCredentials == null)
            {
                throw new ArgumentNullException(nameof(TokenProviderSettings.SigningCredentials));
            }

            if (options.NonceGenerator == null)
            {
                throw new ArgumentNullException(nameof(TokenProviderSettings.NonceGenerator));
            }
        }
    }
}