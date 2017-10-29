using System.Security.Claims;
using System.Threading.Tasks;
using Quotes.Domain.Models;

namespace Quotes.Core.Services.Security
{
    public interface IIdentityService
    {
        Task<ClaimsIdentity> GetIdentity(string username, string password);

        Task<User> CreateIdentity(string username, string password);
    }
}