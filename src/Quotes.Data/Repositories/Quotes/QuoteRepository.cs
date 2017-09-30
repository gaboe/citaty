using MongoDB.Bson;
using Quotes.Data.Context;
using Quotes.Data.Domain.Models;

namespace Quotes.Data.Repositories.Quotes
{
    internal class QuoteRepository : BaseRepository<Quote, ObjectId>, IQuoteRepository
    {
        public QuoteRepository(IDbContextProvider<Quote> contextProvider) : base(contextProvider)
        {
        }
    }
}