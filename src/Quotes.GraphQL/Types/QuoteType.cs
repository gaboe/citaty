using GraphQL.Types;
using Quotes.Core.Services.Channels;
using Quotes.Core.Services.Users;
using Quotes.Domain.Models;

namespace Quotes.GraphQL.Types
{
    public class QuoteType : ObjectGraphType<Quote>
    {
        public QuoteType(IChannelService channelService)
        {
            Field(x => x.QuoteID).Description("ID of quote");
            Field(x => x.Title).Description("The name of the quote");
            Field(x => x.Content).Description("Content of the quote");
            Field(
                Name = "ChannelID",
                quote => quote.ChannelID.ToString()
            );
            Field<ChannelType>()
                .Name(nameof(Channel))
                .Resolve(context => channelService.Get(context.Source.ChannelID));
        }
    }
}