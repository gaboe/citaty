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
            return _quoteRepository.GetQuotesByChannelID(channelID);
        }

        public Task<Quote> Add(Quote quote)
        {
            return Task.FromResult(_quoteRepository.Add(quote));
        }

        public Task<List<Quote>> GetAll()
        {
            return _quoteRepository.GetAll();
        }
    }
}