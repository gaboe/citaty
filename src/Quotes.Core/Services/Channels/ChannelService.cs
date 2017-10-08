using MongoDB.Bson;
using Quotes.Data.Repositories.Channels;
using Quotes.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Quotes.Core.Services.Channels
{
    public class ChannelService : IChannelService
    {
        private readonly IChannelRepository _channelRepository;

        public ChannelService(IChannelRepository channelRepository)
        {
            _channelRepository = channelRepository;
        }

        public Task<Channel> Get(ObjectId channelID)
        {
            return _channelRepository.Get(channelID);
        }

        public Task<Channel> Get(string channelID)
        {
            return _channelRepository.Get(channelID);
        }

        public Task<List<Channel>> GetAll()
        {
            return _channelRepository.GetAll();
        }

        public Task<Channel> Add(Channel channel)
        {
            return Task.FromResult(_channelRepository.Add(channel));
        }

        public Task<Channel> GetByTitle(string title)
        {
            return _channelRepository.GetByTitle(title);
        }

        public Task<List<Channel>> GetMany(IEnumerable<ObjectId> channelIDs)
        {
            return _channelRepository.GetMany(channelIDs);
        }
    }
}