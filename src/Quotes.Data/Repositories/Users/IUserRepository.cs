using System.Threading.Tasks;
using MongoDB.Bson;
using Quotes.Domain.Models;

namespace Quotes.Data.Repositories.Users
{
    public interface IUserRepository : IBaseRepository<User, ObjectId>
    {
        Task<User> GetUserByLogin(string login);
    }
}