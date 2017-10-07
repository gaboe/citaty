using GraphQL.Types;
using Quotes.Domain.Models;

namespace Quotes.GraphQL.Types
{
    public class UserType : ObjectGraphType<User>
    {
        public UserType()
        {
            
        }
    }
}
