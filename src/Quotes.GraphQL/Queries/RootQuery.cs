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
            Field<QuoteType>()
                .Name("quote")
                .Resolve(context => quoteService.GetAll().Result.First());

            Field<ListGraphType<QuoteType>>()
                .Name("quotes")
                .Resolve(context => quoteService.GetAll());

            Field<ChannelType>()
                .Name("channel")
                .Resolve(context => channelService.GetAll().Result.First());

            Field<ListGraphType<ChannelType>>()
                .Name("channels")
                .Resolve(context => channelService.GetAll());

            Field<ListGraphType<UserType>>()
                .Name("users")
                .Resolve(context => userService.GetAll());

            Field<UserType>(
                name: "user",
                arguments: new QueryArguments(new QueryArgument<StringGraphType> { Name = "login" }),
                resolve: context =>
                {
                    var login = context.GetArgument<string>("login");
                    return userService.GetUserByLogin(login);
                });
        }
    }
}