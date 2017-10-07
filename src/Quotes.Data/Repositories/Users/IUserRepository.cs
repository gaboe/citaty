using MongoDB.Bson;
using Quotes.Domain.Models;

namespace Quotes.Data.Repositories.Users
{
    public interface IUserRepository : IBaseRepository<User, ObjectId>
    {
    }
}
