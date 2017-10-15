using System.Security.Claims;
using System.Threading.Tasks;

namespace Quotes.Core.Services.Security
{
    public interface ISecurityService
    {
        Task<ClaimsIdentity> GetIdentity(string username, string password);
    }
}
