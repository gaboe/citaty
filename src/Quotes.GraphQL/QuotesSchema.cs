using GraphQL.Types;
using Quotes.GraphQL.Queries;
using System;

namespace Quotes.GraphQL
{
    public class QuotesSchema : Schema
    {
        public QuotesSchema(Func<Type, GraphType> resolveType) : base(resolveType)
        {
            Query = (RootQuery) resolveType(typeof(RootQuery));
        }
    }
}