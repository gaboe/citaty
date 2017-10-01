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
            //Field(x => x.Quotes, true, typeof(ListGraphType<QuoteType>)).Description("Enumerable of quotes");
            Field<ListGraphType<QuoteType>>(
                nameof(Channel.Quotes).ToLower(),
                resolve: c => quotesService.GetQuotesByChannelID(c.Source.ID));
            Field(x => x.Title).Description("Title of quotes");
        }
    }
}