using GraphQL.Types;
using Quotes.Domain.Models;

namespace Quotes.GraphQL.Types
{
    public class QuoteType : ObjectGraphType<Quote>
    {
        public QuoteType()
        {
            Field(x => x.QuoteID).Description("ID of Quote");
            Field(x => x.Title).Description("The name of the Quote");
        }
    }
}