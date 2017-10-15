using Microsoft.AspNetCore.Identity;
using Quotes.Core.Services.Users;
using Quotes.Domain.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Quotes.Core.Services.Security
{
    public class UserStore : IUserStore<User>
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
            return Task.FromResult(_userService.GetUserByLogin(user.Login).Result.UserID);
        }

        public Task<string> GetUserNameAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(_userService.GetByID(user.Id).Result.Login);
        }

        public Task SetUserNameAsync(User user, string userName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetNormalizedUserNameAsync(User user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetNormalizedUserNameAsync(User user, string normalizedName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> CreateAsync(User user, CancellationToken cancellationToken)
        {
            _userService.CreateUser(user);
            var result = new IdentityResult {Errors = { },};
            return Task.FromResult(result);
        }

        public Task<IdentityResult> UpdateAsync(User user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> DeleteAsync(User user, CancellationToken cancellationToken)
        {
            _userService.DeleteUser(user);

            var result = new IdentityResult {Errors = { },};
            return Task.FromResult(result);
        }

        public Task<User> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            return _userService.GetByID(userId);
        }

        public Task<User> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            return _userService.GetUserByLogin(normalizedUserName);
        }
    }
}