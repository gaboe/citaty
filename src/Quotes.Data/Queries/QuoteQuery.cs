using System.Linq;
using GraphQL.Types;
using Quotes.Data.GraphQL.Models;
using Quotes.Data.Repositories.Channels;
using Quotes.Data.Repositories.Quotes;

namespace Quotes.Data.Queries
{
    public class QuoteQuery : ObjectGraphType
    {
        public QuoteQuery(IQuoteRepository quoteRepository, IChannelRepository channelRepository)
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