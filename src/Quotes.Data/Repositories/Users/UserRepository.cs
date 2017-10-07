using MongoDB.Bson;
using MongoDB.Driver;
using Quotes.Data.Context;
using Quotes.Domain.Models;
using System.Threading.Tasks;

namespace Quotes.Data.Repositories.Users
{
    public class UserRepository : BaseRepository<User, ObjectId>, IUserRepository
    {
        public UserRepository(IDbContextProvider<User> contextProvider) : base(contextProvider)
        {
        }

        public Task<User> GetUserByLogin(string login)
        {
            return Collection.FindAsync(x => x.Login.Equals(login)).Result.SingleAsync();
        }
    }
}