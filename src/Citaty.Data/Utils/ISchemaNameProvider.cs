using MongoDB.Bson;
using Quotes.Data.Domain;

namespace Quotes.Data.Utils
{
    public interface ISchemaNameProvider<TEntity>
        where TEntity : IEntity<ObjectId>
    {
        string GetSchemaName();
    }
}
