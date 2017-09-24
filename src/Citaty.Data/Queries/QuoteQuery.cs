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
                resolve: context => quoteRepository.GetByID("59c7a24ae651d381070f54b5")
            );
        }
    }
}