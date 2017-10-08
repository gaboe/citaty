using System.Net.Http;

namespace Quotes.Core.Creators
{
    public interface IQueryCreator
    {
        StringContent CreateQuery(string query);
    }
}
