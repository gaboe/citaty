﻿using GraphQL.Types;
using Quotes.Core.Services.Channels;
using Quotes.Domain.Models;

namespace Quotes.GraphQL.Types
{
    public class QuoteType : ObjectGraphType<Quote>
    {
        public QuoteType(IChannelService channelService)
        {
            Name = "Quote";
            Description = "One quote in channel";

            Field(x => x.QuoteID).Description("ID of quote");
            Field(x => x.Content).Description("Content of the quote");

            Field<StringGraphType>()
                .Name("OwningChannelID")
                .Description("ID of channel where current quote belongs")
                .Resolve(context => context.Source.OwningChannelID);

            Field<ChannelType>()
                .Name(nameof(Channel))
                .Resolve(context => channelService.Get(context.Source.ChannelID));
        }
    }
}