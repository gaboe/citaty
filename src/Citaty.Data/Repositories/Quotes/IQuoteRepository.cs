using System.Threading.Tasks;
using Quotes.Data.Domain.Models;

namespace Quotes.Data.Repositories.Quotes
{
    public interface IQuoteRepository
    {
        Task<Quote> GetByID(int id);
    }
}