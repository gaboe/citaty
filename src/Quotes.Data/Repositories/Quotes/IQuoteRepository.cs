using MongoDB.Bson;
using Quotes.Data.Domain.Models;

namespace Quotes.Data.Repositories.Quotes
{
    public interface IQuoteRepository : IBaseRepository<Quote, ObjectId> { }
}