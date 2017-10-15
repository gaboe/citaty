using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;
using Quotes.Domain.Settings;

namespace Quotes.Core.Middlewares.Security
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseTokenProvider(this IApplicationBuilder builder,
            TokenProviderSettings parameters)
        {
            return builder.UseMiddleware<TokenProviderMiddleware>(Options.Create(parameters));
        }
    }
}