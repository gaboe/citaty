using MongoDB.Driver;
using Quotes.Data.Context;
using Quotes.Data.Domain.Models;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace Quotes.Data.Repositories.Quotes
{
    internal class QuoteRepository : IQuoteRepository
    {
        private readonly IMongoCollection<Quote> _quotesCollection;

        public QuoteRepository(IQuotesContextProvider contextProvider)
        {
            _quotesCollection = contextProvider.GetContext();
        }

        public Task<Quote> GetByID(string id)
        {
            var objectID = ObjectId.Parse(id);
            return _quotesCollection.FindAsync(q => q.QuoteID.Equals(objectID)).Result.SingleAsync();
        }
    }
}