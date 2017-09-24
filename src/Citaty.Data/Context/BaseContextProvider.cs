using MongoDB.Bson;
using MongoDB.Driver;
using Quotes.Data.Domain;
using Quotes.Data.Infrastructure;
using Quotes.Data.Utils;

namespace Quotes.Data.Context
{
    public class BaseContextProvider<TEntity> : IBaseContextProvider<TEntity>
        where TEntity : IEntity<ObjectId>, new()
    {
        private readonly ISchemaNameProvider<TEntity> _shemaNameProvider;

        public BaseContextProvider(ISchemaNameProvider<TEntity> shemaNameProvider)
        {
            _shemaNameProvider = shemaNameProvider;
        }

        public IMongoCollection<TEntity> GetContext()
        {
            var shema = _shemaNameProvider.GetSchemaName();
            return new MongoClient().GetDatabase(DbConstants.DbName)
                .GetCollection<TEntity>(shema);
        }
    }
}