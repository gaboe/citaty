using GraphQL.Types;
using Quotes.Core.Services.Channels;
using Quotes.Core.Services.Quotes;
using Quotes.Core.Services.Users;
using Quotes.Domain.Models;
using Quotes.GraphQL.Types;

namespace Quotes.GraphQL.Mutations
{
    public class RootMutation : ObjectGraphType<object>
    {
        public RootMutation(IUserService userService, IQuoteService quoteService, IChannelService channelService)
        {
            Name = "Mutation";

            Field<UserType>(
                "createUser",
                arguments: new QueryArguments(new QueryArgument<StringGraphType> {Name = "login"}),
                resolve: context =>
                {
                    var login = context.GetArgument<string>("login");
                    return userService.AddUser(new User {Login = login.ToString()});
                });

            Field<QuoteType>(
                "createQuote",
                arguments: new QueryArguments(new QueryArgument<QuoteInputType> {Name = "input"}),
                resolve: context =>
                {
                    var quote = context.GetArgument<Quote>("input");
                    return quoteService.Add(quote);
                });

            Field<ChannelType>(
                "createChannel",
                arguments: new QueryArguments(new QueryArgument<StringGraphType> {Name = "title"}),
                resolve: context =>
                {
                    var title = context.GetArgument<string>("title");
                    return channelService.Add(new Channel {Title = title});
                });
        }
    }
}