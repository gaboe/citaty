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
            var result = _userService.GetUserByUsername(user.UserName).Result;
            return Task.FromResult(result?.UserID);
        }

        public Task<string> GetUserNameAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(_userService.Exists(user.Id)
                ? _userService.GetByID(user.Id).Result.UserName
                : string.Empty);
        }

        public Task SetUserNameAsync(User user, string userName, CancellationToken cancellationToken)
        {
            _userService.SetUsername(user.Id, userName);
            return Task.CompletedTask;
        }

        public Task<string> GetNormalizedUserNameAsync(User user, CancellationToken cancellationToken)
        {
            var username = _userService.GetByID(user.Id).Result.UserName.ToUpper();
            return Task.FromResult(username);
        }

        public Task SetNormalizedUserNameAsync(User user, string normalizedName, CancellationToken cancellationToken)
        {
            _userService.SetUsername(user.Id, normalizedName);
            return Task.CompletedTask;
        }

        public Task<IdentityResult> CreateAsync(User user, CancellationToken cancellationToken)
        {
            _userService.CreateUser(user);
            var result = new IdentityResult();
            return Task.FromResult(result);
        }

        public Task<IdentityResult> UpdateAsync(User user, CancellationToken cancellationToken)
        {
            _userService.Replace(user);
            var result = new IdentityResult();
            return Task.FromResult(result);
        }

        public Task<IdentityResult> DeleteAsync(User user, CancellationToken cancellationToken)
        {
            _userService.DeleteUser(user);

            var result = new IdentityResult();
            return Task.FromResult(result);
        }

        public Task<User> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            return _userService.GetByID(userId);
        }

        public Task<User> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            return _userService.GetUserByUsername(normalizedUserName);
        }

        public Task SetPasswordHashAsync(User user, string passwordHash, CancellationToken cancellationToken)
        {
            _userService.SetPasswordHash(user.Id, passwordHash);
            return Task.CompletedTask;
        }

        public Task<string> GetPasswordHashAsync(User user, CancellationToken cancellationToken)
        {
            var passwordHash = _userService.GetByID(user.Id).Result.PasswordHash;
            return Task.FromResult(passwordHash);
        }

        public Task<bool> HasPasswordAsync(User user, CancellationToken cancellationToken)
        {
            var userPassword = _userService.GetByID(user.Id).Result.Password;
            return Task.FromResult(string.IsNullOrEmpty(userPassword));
        }
    }
}