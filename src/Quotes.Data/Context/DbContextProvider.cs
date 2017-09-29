using MongoDB.Driver;
using Quotes.Data.Utils;

namespace Quotes.Data.Context
{
    public class DbContextProvider<TEntity> : IDbContextProvider<TEntity>
        where TEntity : class
    {
        private readonly ISchemaNameProvider<TEntity> _shemaNameProvider;
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public DbContextProvider(
            ISchemaNameProvider<TEntity> shemaNameProvider,
            IDbConnectionFactory dbConnectionFactory)
        {
            _shemaNameProvider = shemaNameProvider;
            _dbConnectionFactory = dbConnectionFactory;
        }

        public IMongoCollection<TEntity> GetContext()
        {
            var shema = _shemaNameProvider.GetSchemaName();
            return _dbConnectionFactory
                .GetConnection()
                .GetCollection<TEntity>(shema);
        }
    }
}