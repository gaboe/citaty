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
            return user != null
                ? Task.FromResult(new ClaimsIdentity(new GenericIdentity(username, "Token"), new Claim[] { }))
                : Task.FromResult<ClaimsIdentity>(null);
        }
    }
}