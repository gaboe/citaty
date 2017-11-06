using Quotes.Domain.Models;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Quotes.Core.Services.Security
{
    public interface IIdentityService
    {
        Task<ClaimsIdentity> GetIdentity(string username, string password);

        Task<User> CreateIdentity(string username, string password);
    }
}