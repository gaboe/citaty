using GraphQL.Types;
using Quotes.Core.Services.Channels;
using Quotes.Core.Services.Quotes;
using Quotes.Core.Services.Security;
using Quotes.Domain.Models;
using Quotes.GraphQL.Types;

namespace Quotes.GraphQL.Mutations
{
    public class RootMutation : ObjectGraphType<object>
    {
        public RootMutation(
            IQuoteService quoteService,
            IChannelService channelService,
            IIdentityService identityService)
        {
            Name = "Mutation";

            Field<UserType>(
                "createUser",
                arguments: new QueryArguments(new QueryArgument<StringGraphType> { Name = "username" },
                    new QueryArgument<StringGraphType> { Name = "password" }),
                resolve: context =>
                {
                    var userName = context.GetArgument<string>("username");
                    var password = context.GetArgument<string>("password");
                    var user = identityService.CreateIdentity(userName, password).Result;
                    return user;
                });

            Field<QuoteType>(
                "createQuote",
                arguments: new QueryArguments(new QueryArgument<QuoteInputType> { Name = "input" }),
                resolve: context =>
                {
                    var quote = context.GetArgument<Quote>("input");
                    return quoteService.Add(quote);
                });

            Field<ChannelType>(
                "createChannel",
                arguments: new QueryArguments(new QueryArgument<StringGraphType> { Name = "title" }),
                resolve: context =>
                {
                    var title = context.GetArgument<string>("title");
                    return channelService.Add(new Channel { Title = title });
                });
        }
    }
}