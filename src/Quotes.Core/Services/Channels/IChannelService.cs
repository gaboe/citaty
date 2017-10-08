using MongoDB.Bson;
using Quotes.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Quotes.Core.Services.Channels
{
    public interface IChannelService
    {
        Task<Channel> Get(ObjectId channelID);
        Task<Channel> Get(string channelID);
        Task<List<Channel>> GetAll();
        void Add(Channel channel);
        Task<Channel> GetByTitle(string title);
        Task<List<Channel>> GetMany(IEnumerable<ObjectId> channelIDs);
    }
}