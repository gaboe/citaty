using MongoDB.Driver;
using Quotes.Data.Context;
using Quotes.Data.Domain.Models;
using System.Threading.Tasks;
using MongoDB.Bson;
using Microsoft.Extensions.Logging;

namespace Quotes.Data.Repositories.Quotes
{
    internal class QuoteRepository : IQuoteRepository
    {
        private readonly IMongoCollection<Quote> _quotesCollection;
        private readonly ILogger _logger;

        public QuoteRepository(IQuotesContextProvider contextProvider, ILogger logger)
        {
            _logger = logger;
            _quotesCollection = contextProvider.GetContext();
        }

        public Task<Quote> GetByID(string id)
        {
            _logger.LogInformation($"Get quote with id {id}");
            var objectID = ObjectId.Parse(id);
            return _quotesCollection.FindAsync(q => q.QuoteID.Equals(objectID)).Result.SingleAsync();
        }
    }
}