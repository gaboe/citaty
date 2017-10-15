using MongoDB.Bson;
using Quotes.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Quotes.Data.Repositories.Quotes
{
    public interface IQuoteRepository : IBaseRepository<Quote, ObjectId>
    {
        Task<List<Quote>> GetQuotesByChannelID(ObjectId channelID);
    }
}