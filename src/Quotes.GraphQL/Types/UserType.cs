using System.Linq;
using GraphQL;
using GraphQL.Types;
using Quotes.Core.Services.Channels;
using Quotes.Domain.Models;

namespace Quotes.GraphQL.Types
{
    public class UserType : ObjectGraphType<User>
    {
        public UserType(IChannelService channelService)
        {
            Field(x => x.UserID);
            Field(x => x.Login);
            Field<ListGraphType<ChannelType>>(
                Name = nameof(User.FavouriteChannels).ToCamelCase(),
                Description = "Favourite channels of user",
                resolve: c => channelService.GetMany(c.Source.FavouriteChannels.Select(x => x.ID))
            );
        }
    }
}