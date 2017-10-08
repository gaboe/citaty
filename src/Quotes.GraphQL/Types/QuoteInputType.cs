using GraphQL.Types;

namespace Quotes.GraphQL.Types
{
    public class QuoteInputType : InputObjectGraphType
    {
        public QuoteInputType()
        {
            Name = "QuoteInput";
            Field<NonNullGraphType<StringGraphType>>("content");
            Field<NonNullGraphType<StringGraphType>>("owningChannelID");
        }
    }
}