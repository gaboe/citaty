using GraphQL.Types;

namespace Quotes.Api.Infrastructure
{
    public interface ISchemaProvider
    {
        ISchema GetRootSchema();
    }
}