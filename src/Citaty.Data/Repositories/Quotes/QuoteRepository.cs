using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using Quotes.Data.Context;
using Quotes.Data.Domain.Models;

namespace Quotes.Data.Repositories.Quotes
{
    internal class QuoteRepository : BaseRepository<Quote,ObjectId,QuoteRepository>, IQuoteRepository
    {
        public QuoteRepository(ILogger<QuoteRepository> logger,IDbContextProvider<Quote> contextProvider)
            : base(logger,contextProvider)
        {}
    }
}