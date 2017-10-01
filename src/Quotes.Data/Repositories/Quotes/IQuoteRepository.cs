using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using Quotes.Domain.Models;

namespace Quotes.Data.Repositories.Quotes
{
    public interface IQuoteRepository : IBaseRepository<Quote, ObjectId> {
        Task<List<Quote>> GetQuotesByChannelID(ObjectId channelID);
    }
}