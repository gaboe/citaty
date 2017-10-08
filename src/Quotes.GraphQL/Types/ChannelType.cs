using GraphQL.Types;
using Quotes.Core.Services.Quotes;
using Quotes.Domain.Models;

namespace Quotes.GraphQL.Types
{
    public class ChannelType : ObjectGraphType<Channel>
    {
        public ChannelType(IQuoteService quotesService)
        {
            Field(x => x.ChannelID).Description("ID of channel");

            Field(x => x.Title).Description("Title of quotes");

            Field<ListGraphType<QuoteType>>()
                .Name(nameof(Channel.Quotes))
                .Description("List of quotes")
                .Resolve(context => quotesService.GetQuotesByChannelID(context.Source.ID));
        }
    }
}