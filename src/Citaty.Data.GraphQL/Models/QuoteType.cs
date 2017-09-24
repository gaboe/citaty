using GraphQL.Types;
using Quotes.Data.Domain.Models;

namespace Quotes.Data.GraphQL.Models
{
    public class QuoteType : ObjectGraphType<Quote>
    {
        public QuoteType()
        {
            Field(x => x.Id).Description("ID of Quote");
            Field(x => x.Title).Description("The name of the Quoute");
        }
    }
}
