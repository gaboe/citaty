using MongoDB.Bson;
using Quotes.Data.Domain;

namespace Quotes.Data.Utils
{
    public class SchemaNameProvider<TEntity> : ISchemaNameProvider<TEntity> where TEntity : IEntity<ObjectId>
    {
        public string GetSchemaName()
        {
            return typeof(TEntity).Name.EndsWith("s")
                ? $"{typeof(TEntity).Name.ToLower()}es"
                : $"{typeof(TEntity).Name.ToLower()}s";
        }
    }
}