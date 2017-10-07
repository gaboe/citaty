using GraphQL.Types;
using Quotes.Core.Services.Channels;
using Quotes.Core.Services.Quotes;
using Quotes.Core.Services.Users;
using Quotes.GraphQL.Types;
using System.Linq;

namespace Quotes.GraphQL.Queries
{
    public class RootQuery : ObjectGraphType
    {
        public RootQuery(
            IQuoteService quoteService,
            IChannelService channelService,
            IUserService userService
        )
        {
            Field<QuoteType>(
                "quote",
                resolve: context => quoteService.GetAll().Result.First()
            );
            Field<ListGraphType<QuoteType>>(
                "quotes",
                resolve: context => quoteService.GetAll()
            );
            Field<ChannelType>(
                "channel",
                resolve: context => channelService.GetAll().Result.First()
            );
            Field<ListGraphType<ChannelType>>(
                "channels",
                resolve: context => channelService.GetAll()
            );
            Field<ListGraphType<UserType>>(
                "users",
                resolve: context => userService.GetAll()
            );
        }
    }
}