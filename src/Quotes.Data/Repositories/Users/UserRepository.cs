using MongoDB.Bson;
using Quotes.Data.Context;
using Quotes.Domain.Models;

namespace Quotes.Data.Repositories.Users
{
    public class UserRepository : BaseRepository<User,ObjectId>, IUserRepository
    {
        public UserRepository(IDbContextProvider<User> contextProvider) : base(contextProvider)
        {
        }
    }
}
