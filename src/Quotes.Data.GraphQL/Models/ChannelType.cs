using GraphQL.Types;
using Quotes.Data.Domain.Models;

namespace Quotes.Data.GraphQL.Models
{
    public class ChannelType : ObjectGraphType<Channel>
    {
        public ChannelType()
        {
            Field(x => x.ChannelID).Description("ID of channel");
            Field(x => x.Quotes, true, typeof(ListGraphType<QuoteType>)).Description("Enumerable of quotes");
            Field(x => x.Title).Description("Title of quotes");
        }
    }
}