using System;
using GraphQL.Types;
using Quotes.GraphQL.Queries;

namespace Quotes.GraphQL.Schemas
{
    public class QuotesSchema : Schema
    {
        public QuotesSchema(Func<Type, GraphType> resolveType) : base(resolveType)
        {
            Query = (RootQuery) resolveType(typeof(RootQuery));
        }
    }
}