using Microsoft.AspNetCore.Identity;
using Quotes.Core.Services.Users;
using Quotes.Domain.Models;
using System.Threading;
using System.Threading.Tasks;

namespace Quotes.Core.Services.Security
{
    public class UserStore : IUserPasswordStore<User>
    {
        private readonly IUserService _userService;

        public UserStore(IUserService userService)
        {
            _userService = userService;
        }

        public void Dispose()
        {
        }

        public Task<string> GetUserIdAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.UserID);
        }

        public Task<string> GetUserNameAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.UserName);
        }

        public Task SetUserNameAsync(User user, string userName, CancellationToken cancellationToken)
        {
            user.UserName = userName;
            return Task.CompletedTask;
        }

        public Task<string> GetNormalizedUserNameAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.NormalizedUserName);
        }

        public Task SetNormalizedUserNameAsync(User user, string normalizedName, CancellationToken cancellationToken)
        {
            user.NormalizedUserName = normalizedName;
            return Task.CompletedTask;
        }

        public Task<IdentityResult> CreateAsync(User user, CancellationToken cancellationToken)
        {
            _userService.CreateUser(user);
            var result = IdentityResult.Success;
            return Task.FromResult(result);
        }

        public Task<IdentityResult> UpdateAsync(User user, CancellationToken cancellationToken)
        {
            _userService.Replace(user);
            var result = IdentityResult.Success;
            return Task.FromResult(result);
        }

        public Task<IdentityResult> DeleteAsync(User user, CancellationToken cancellationToken)
        {
            _userService.DeleteUser(user);
            var result = IdentityResult.Success;
            return Task.FromResult(result);
        }

        public Task<User> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            return _userService.GetByID(userId);
        }

        public Task<User> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            return _userService.GetUserByNormalizedUsername(normalizedUserName);
        }

        public Task SetPasswordHashAsync(User user, string passwordHash, CancellationToken cancellationToken)
        {
            user.PasswordHash = passwordHash;
            return Task.CompletedTask;
        }

        public Task<string> GetPasswordHashAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PasswordHash);
        }

        public Task<bool> HasPasswordAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(!string.IsNullOrEmpty(user.PasswordHash));
        }
    }
}