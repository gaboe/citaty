using MongoDB.Driver;
using Quotes.Data.Infrastructure;

namespace Quotes.Data.Context
{
    internal class DbConnectionFactory : IDbConnectionFactory
    {
        private readonly string _connectionString;
        private readonly string _databaseName;

        public DbConnectionFactory(string connectionString, string databaseName)
        {
            _connectionString = connectionString;
            _databaseName = databaseName;
        }

        public IMongoDatabase GetConnection()
        {
           return new MongoClient(_connectionString).GetDatabase(_databaseName);
        }
    }
}
