using Citaty.Data.Domain.Models;
using GraphQL.Types;

namespace Citaty.Data.GraphQL.Models
{
    public class QuoteType : ObjectGraphType<Quote>
    {
        public QuoteType()
        {
            Field(x => x.QuoteID).Description("ID of Quote");
            Field(x => x.Title).Description("The name of the Quoute");
        }
    }
}
