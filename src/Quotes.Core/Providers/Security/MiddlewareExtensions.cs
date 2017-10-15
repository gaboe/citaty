using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;

namespace Quotes.Core.Providers.Security
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseTokenProvider(this IApplicationBuilder builder,
            TokenProviderOptions parameters)
        {
            return builder.UseMiddleware<TokenProviderMiddleware>(Options.Create(parameters));
        }
    }
}