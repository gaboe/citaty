using Quotes.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Quotes.Core.Services.Channels
{
    public interface IChannelService
    {
        Task<List<Channel>> GetAll();
        void Add(Channel channel);
        Task<Channel> GetByTitle(string title);
    }
}