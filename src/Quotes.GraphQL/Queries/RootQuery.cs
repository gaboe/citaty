using GraphQL.Types;
using Quotes.GraphQL.Types;
using System.Linq;
using Quotes.Data.Repositories.Channels;
using Quotes.Data.Repositories.Quotes;

namespace Quotes.GraphQL.Queries
{
    public class RootQuery : ObjectGraphType
    {
        public RootQuery(IQuoteRepository quoteRepository, IChannelRepository channelRepository)
        {
            Field<QuoteType>(
                "quote",
                resolve: context => quoteRepository.GetAll().Result.First()
            );
            Field<ListGraphType<QuoteType>>(
                "quotes",
                resolve: context => quoteRepository.GetAll()
            );
            Field<ChannelType>(
                "channel",
                resolve: context => channelRepository.GetAll().Result.First()
            );
            Field<ListGraphType<ChannelType>>(
                "channels",
                resolve: context => channelRepository.GetAll()
            );
        }
    }
}