using MongoDB.Bson;
using Quotes.Data.Repositories.Quotes;
using Quotes.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Quotes.Core.Services.Quotes
{
    public class QuoteService : IQuoteService
    {
        private readonly IQuoteRepository _quoteRepository;

        public QuoteService(IQuoteRepository quoteRepository)
        {
            _quoteRepository = quoteRepository;
        }

        public Task<List<Quote>> GetQuotesByChannelID(ObjectId channelID)
        {
            return _quoteRepository.GetQuoteByChannelID(channelID);
        }

        public void Add(Quote quote)
        {
            _quoteRepository.Add(quote);
        }
    }
}