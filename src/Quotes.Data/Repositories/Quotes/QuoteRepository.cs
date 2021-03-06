﻿using MongoDB.Bson;
using MongoDB.Driver;
using Quotes.Data.Context;
using Quotes.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Quotes.Data.Repositories.Quotes
{
    internal class QuoteRepository : BaseRepository<Quote, ObjectId>, IQuoteRepository
    {
        public QuoteRepository(IDbContextProvider<Quote> contextProvider) : base(contextProvider)
        {
        }

        public Task<List<Quote>> GetQuotesByChannelID(ObjectId channelID)
        {
            return Collection.FindAsync(x => x.ChannelID.Equals(channelID)).Result.ToListAsync();
        }
    }
}