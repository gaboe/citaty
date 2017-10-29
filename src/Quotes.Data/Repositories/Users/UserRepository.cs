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

        public void SetPasswordHash(ObjectId id, string passwordHash)
        {
            Collection.UpdateOne(user => user.Id.Equals(id),
                Builders<User>.Update.Set(user => user.PasswordHash, passwordHash));
        }

        public void SetUsername(ObjectId id, string userName)
        {
            Collection.UpdateOneAsync(user => user.Id.Equals(id),
                Builders<User>.Update.Set(user => user.UserName, userName));
        }
    }
}