using Quotes.Data.Repositories.Channels;
using Quotes.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Quotes.Core.Services.Channels
{
    public class ChannelService : IChannelService
    {
        private  readonly IChannelRepository _channelRepository;

        public ChannelService(IChannelRepository channelRepository)
        {
            _channelRepository = channelRepository;
        }

        public Task<List<Channel>> GetAll()
        {
            return _channelRepository.GetAll();
        }

    }
}