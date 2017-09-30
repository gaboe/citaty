using GraphQL.Types;
using Quotes.GraphQL.Queries;

namespace Quotes.Api.Infrastructure
{
    public class SchemaProvider : ISchemaProvider
    {
        private readonly RootQuery _rootQuery;

        public SchemaProvider(RootQuery rootQuery)
        {
            _rootQuery = rootQuery;
        }

        public ISchema GetRootSchema()
        {
            return new Schema
            {
                Query = _rootQuery
            };
        }
    }
}