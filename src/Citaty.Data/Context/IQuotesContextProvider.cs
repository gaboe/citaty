using MongoDB.Driver;
using Quotes.Data.Domain.Models;

namespace Quotes.Data.Context
{
    internal interface IQuotesContextProvider
    {
        IMongoCollection<Quote> GetContext();
    }
}
