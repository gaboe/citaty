using GraphQL.Types;
using Quotes.Domain.Models;

namespace Quotes.GraphQL.Types
{
    public class QuoteType : ObjectGraphType<Quote>
    {
        public QuoteType()
        {
            Field(x => x.QuoteID).Description("ID of quote");
            Field(x => x.Title).Description("The name of the quote");
            Field(x => x.Content).Description("Content of the quote");
            Field(
                Name = "ChannelID",
                quote => quote.ChannelID.ToString()
            );
        }
    }
}