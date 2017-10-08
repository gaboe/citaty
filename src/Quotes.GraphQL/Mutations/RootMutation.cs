using GraphQL.Types;
using Quotes.Core.Services.Users;
using Quotes.Domain.Models;
using Quotes.GraphQL.Types;

namespace Quotes.GraphQL.Mutations
{
    public class RootMutation : ObjectGraphType<object>
    {
        public RootMutation(IUserService userService)
        {
            Name = "Mutation";
            Field<UserType>(
                "createUser",
                arguments: new QueryArguments(new QueryArgument<StringGraphType> { Name = "login" }),
                resolve: context =>
                {
                    var login = context.GetArgument<string>("login");
                    return userService.AddUser(new User {Login = login.ToString()});
                });
        }
    }
}