using MongoDB.Bson;
using Quotes.Domain.Models;
using System.Threading.Tasks;

namespace Quotes.Data.Repositories.Channels
{
    public interface IChannelRepository : IBaseRepository<Channel, ObjectId>
    {
        Task<Channel> GetByTitle(string title);
    }
}