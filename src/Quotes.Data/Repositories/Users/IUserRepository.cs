using MongoDB.Bson;
using Quotes.Domain.Models;
using System.Threading.Tasks;

namespace Quotes.Data.Repositories.Users
{
    public interface IUserRepository : IBaseRepository<User, ObjectId>
    {
        Task<User> GetUserByLogin(string login);

        void SetPasswordHash(ObjectId id, string passwordHash);
    }
}