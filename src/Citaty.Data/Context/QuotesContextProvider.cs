using MongoDB.Driver;
using Quotes.Data.Domain.Models;
using Quotes.Data.Infrastructure;

namespace Quotes.Data.Context
{
    internal class QuotesContextProvider: IQuotesContextProvider
    {
        public IMongoCollection<Quote> GetContext()
        {
            return new MongoClient().GetDatabase(DbConstants.DbName)
                .GetCollection<Quote>(DbConstants.QuotesCollection);
        }
    }
}
