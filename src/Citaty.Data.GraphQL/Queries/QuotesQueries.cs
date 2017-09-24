using Citaty.Data.Domain.Models;
using Citaty.Data.GraphQL.Models;
using GraphQL.Types;

namespace Citaty.Data.GraphQL.Queries
{
    public class QuotesQueries : ObjectGraphType
    {
        public QuotesQueries()
        {
            Field<QuoteType>(
                "quote",
                resolve: context => new Quote {QuoteID = 1, Title = "Ahoj"});
        }
    }
}