using MongoDB.Bson;
using MongoDB.Driver;
using Quotes.Data.Context;
using Quotes.Domain.Models;
using System.Threading.Tasks;

namespace Quotes.Data.Repositories.Channels
{
    public class ChannelRepository : BaseRepository<Channel, ObjectId>, IChannelRepository
    {
        public ChannelRepository(IDbContextProvider<Channel> contextProvider) : base(contextProvider)
        {
        }

        public Task<Channel> GetByTitle(string title)
        {
            return Collection.FindAsync(x => x.Title.Equals(title)).Result.FirstAsync();
        }
    }
}