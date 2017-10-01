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
            Field<ListGraphType<QuoteType>>(
                name: nameof(Channel.Quotes).ToLower(),
                description: "List of quotes",
                resolve: c => quotesService.GetQuotesByChannelID(c.Source.ID));
            Field(x => x.Title).Description("Title of quotes");
        }
    }
}