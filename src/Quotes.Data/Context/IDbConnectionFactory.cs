using MongoDB.Driver;

namespace Quotes.Data.Context
{
    public interface IDbConnectionFactory
    {
        IMongoDatabase GetConnection();
    }
}
