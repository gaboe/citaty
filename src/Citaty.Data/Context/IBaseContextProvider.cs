using MongoDB.Bson;
using MongoDB.Driver;
using Quotes.Data.Domain;

namespace Quotes.Data.Context
{
    public interface IBaseContextProvider<TEntity>
    {
        IMongoCollection<TEntity> GetContext();
    }
}
