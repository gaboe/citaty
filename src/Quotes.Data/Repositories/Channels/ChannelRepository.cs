using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;
using Quotes.Data.Context;
using Quotes.Data.Domain.Models;
using System.Threading.Tasks;

namespace Quotes.Data.Repositories.Channels
{
    public class ChannelRepository : BaseRepository<Channel, ObjectId, ChannelRepository>, IChannelRepository
    {
        public ChannelRepository(ILogger<ChannelRepository> logger, IDbContextProvider<Channel> contextProvider)
            : base(logger, contextProvider)
        {
        }

        public Task<Channel> GetByTitle(string title)
        {
            return Collection.FindAsync(x => x.Title.Equals(title)).Result.FirstAsync();
        }
    }
}