using Citaty.Data.GraphQL.Models;
using Citaty.Data.Repositories.Quotes;
using GraphQL.Types;

namespace Citaty.Data.Queries
{
    public class QuoteQuery : ObjectGraphType 
    {
        public QuoteQuery(IQuoteRepository quoteRepository)
        {
            Field<QuoteType>(
                "quote",
                resolve: context => quoteRepository.GetByID(1)
            );
        }
    }
}