using GraphQL.Types;

namespace Quotes.GraphQL.Types
{
    public class UserInputType : InputObjectGraphType
    {
        public UserInputType()
        {
            Name = "UserInput";
            Field<NonNullGraphType<StringGraphType>>("login");
        }
    }
}