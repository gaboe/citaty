using GraphQL.Types;
using Quotes.GraphQL.Mutations;
using Quotes.GraphQL.Queries;
using System;

namespace Quotes.GraphQL.Schemas
{
    public class QuotesSchema : Schema
    {
        public QuotesSchema(Func<Type, GraphType> resolveType) : base(resolveType)
        {
            Query = (RootQuery) resolveType(typeof(RootQuery));
            Mutation = (RootMutation) resolveType(typeof(RootMutation));
        }
    }
}