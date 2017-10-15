using MongoDB.Driver;

namespace Quotes.Data.Context
{
    public interface IDbContextProvider<TEntity>
    {
        IMongoCollection<TEntity> GetContext();
    }
}