using MongoDB.Bson;
using Quotes.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Quotes.Core.Services.Quotes
{
    public interface IQuoteService
    {
        Task<List<Quote>> GetAll();
        Task<List<Quote>> GetQuotesByChannelID(ObjectId channelID);
        void Add(Quote quote);
    }
}