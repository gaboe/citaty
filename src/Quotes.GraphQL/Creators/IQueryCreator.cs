using System.Net.Http;

namespace Quotes.GraphQL.Creators
{
    public interface IQueryCreator
    {
        StringContent CreateQuery(string query);
    }
}