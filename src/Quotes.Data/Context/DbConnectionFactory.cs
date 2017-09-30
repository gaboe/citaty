using MongoDB.Driver;
using Quotes.Data.Domain.Settings;

namespace Quotes.Data.Context
{
    internal class DbConnectionFactory : IDbConnectionFactory
    {
        private readonly DatabaseSettings _databaseSettings;

        public DbConnectionFactory(DatabaseSettings databaseSettings)
        {
            _databaseSettings = databaseSettings;
        }

        public IMongoDatabase GetConnection()
        {
            return new MongoClient(_databaseSettings.ConnectionString)
                .GetDatabase(_databaseSettings.DatabaseName);
        }
    }
}