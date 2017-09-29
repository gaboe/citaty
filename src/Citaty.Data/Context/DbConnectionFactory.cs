using MongoDB.Driver;
using Quotes.Data.Infrastructure;

namespace Quotes.Data.Context
{
    internal class DbConnectionFactory : IDbConnectionFactory
    {
        private readonly string _connectionString;

        public DbConnectionFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IMongoDatabase GetConnection()
        {
           return new MongoClient(_connectionString).GetDatabase(DbConstants.DbName);
        }
    }
}
