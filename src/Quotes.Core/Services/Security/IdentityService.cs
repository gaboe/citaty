using Microsoft.AspNetCore.Identity;
using Quotes.Domain.Models;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace Quotes.Core.Services.Security
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public IdentityService(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<ClaimsIdentity> GetIdentity(string username, string password)
        {
            var result = await _signInManager.PasswordSignInAsync(username, password, false, false);
            return await (result.Succeeded
                ? Task.FromResult(new ClaimsIdentity(new GenericIdentity(username, "Token"), new Claim[] { }))
                : Task.FromResult<ClaimsIdentity>(null));
        }

        public Task<User> CreateIdentity(string username, string password)
        {
            var identityResult = _userManager.CreateAsync(new User { UserName = username }, password).Result;
            return identityResult.Succeeded
                ? _userManager.FindByNameAsync(username)
                : null;
        }
    }
}