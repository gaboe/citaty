using MongoDB.Driver;
using Quotes.Data.Infrastructure;

namespace Quotes.Data.Context
{
    internal class DbConnectionFactory : IDbConnectionFactory
    {
        public IMongoDatabase GetConnection()
        {
           return new MongoClient("mongodb://localhost:27017/quotesdb").GetDatabase(DbConstants.DbName);
        }
    }
}
