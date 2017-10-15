using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Quotes.Domain.Models;

namespace Quotes.Core.Services.Security
{
    public class SecurityService : ISecurityService
    {
        private readonly UserManager<User> _userManager;

        public SecurityService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public Task<ClaimsIdentity> GetIdentity(string username, string password)
        {
            var user = _userManager.FindByNameAsync(username).Result;
            // Don't do this in production, obviously!
            if (username == "TEST" && password == "TEST123")
            {
                return Task.FromResult(new ClaimsIdentity(new GenericIdentity(username, "Token"), new Claim[] { }));
            }

            // Credentials are invalid, or account doesn't exist
            return Task.FromResult<ClaimsIdentity>(null);
        }
    }
}