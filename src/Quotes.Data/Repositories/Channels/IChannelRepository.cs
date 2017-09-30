using System.Threading.Tasks;
using MongoDB.Bson;
using Quotes.Data.Domain.Models;

namespace Quotes.Data.Repositories.Channels
{
    public interface IChannelRepository : IBaseRepository<Channel, ObjectId>
    {
        Task<Channel> GetByTitle(string title);
    }
}