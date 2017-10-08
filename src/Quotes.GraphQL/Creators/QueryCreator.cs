using System.Net.Http;
using System.Text;

namespace Quotes.Core.Creators
{
    public class QueryCreator : IQueryCreator
    {
        public StringContent CreateQuery(string query)
        {
            return new StringContent($"{{\"query\":\"{query}\"}}", Encoding.UTF8, "application/json");
        }
    }
}