using System.Threading.Tasks;
using Citaty.Data.Domain.Models;

namespace Citaty.Data.Repositories.Quotes
{
    public interface IQuoteRepository
    {
        Task<Quote> GetByID(int id);
    }
}