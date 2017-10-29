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

        public Task<User> GetUserByLogin(string username)
        {
            return Collection
                .FindAsync(x => x.UserName.Equals(username))
                .Result
                .SingleOrDefaultAsync();
        }

        public Task<User> GetUserByNormalizedUsername(string normalizedUserName)
        {
            return Collection
                .FindAsync(user => user.NormalizedUserName.Equals(normalizedUserName))
                .Result
                .SingleOrDefaultAsync();
        }
    }
}