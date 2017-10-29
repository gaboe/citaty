using MongoDB.Bson;
using Quotes.Domain.Models;
using System.Threading.Tasks;

namespace Quotes.Data.Repositories.Users
{
    public interface IUserRepository : IBaseRepository<User, ObjectId>
    {
        Task<User> GetUserByLogin(string username);

        void SetPasswordHash(ObjectId id, string passwordHash);

        void SetUsername(ObjectId id, string userName);
    }
}