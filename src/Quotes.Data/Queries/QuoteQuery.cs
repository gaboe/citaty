using GraphQL.Types;
using Quotes.Data.GraphQL.Models;
using Quotes.Data.Repositories.Quotes;

namespace Quotes.Data.Queries
{
    public class QuoteQuery : ObjectGraphType 
    {
        public QuoteQuery(IQuoteRepository quoteRepository)
        {
            Field<QuoteType>(
                "quote",
                resolve: context => quoteRepository.GetAll()
            );
        }
    }
}