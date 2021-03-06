﻿using GraphQL;
using GraphQL.Types;
using Quotes.Core.Services.Channels;
using Quotes.Domain.Models;
using System.Linq;

namespace Quotes.GraphQL.Types
{
    public class UserType : ObjectGraphType<User>
    {
        public UserType(IChannelService channelService)
        {
            Field(x => x.UserID);
            Field(x => x.UserName);
            Field<ListGraphType<ChannelType>>(
                Name = nameof(User.FavouriteChannels).ToCamelCase(),
                Description = "Favourite channels of user",
                resolve: c => channelService.GetMany(c.Source.FavouriteChannels.Select(x => x.Id))
            );
        }
    }
}