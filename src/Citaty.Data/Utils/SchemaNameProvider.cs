using MongoDB.Bson;
using Quotes.Data.Domain;

namespace Quotes.Data.Utils
{
    public class SchemaNameProvider<TEntity> : ISchemaNameProvider<TEntity> where TEntity : IEntity<ObjectId>
    {
        public string GetSchemaName()
        {
            return nameof(TEntity).EndsWith("s")
                ? $"{nameof(TEntity).ToLower()}es"
                : $"{nameof(TEntity).ToLower()}s";
        }
    }
}