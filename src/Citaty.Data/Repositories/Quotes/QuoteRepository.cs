﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Quotes.Data.Domain.Models;

namespace Quotes.Data.Repositories.Quotes
{
    internal class QuoteRepository : IQuoteRepository
    {
        private readonly IList<Quote> _qoute
            = new List<Quote> {new Quote {QuoteID = 1, Title = "Ahoj"}};

        public Task<Quote> GetByID(int id)
        {
            return Task.FromResult(_qoute.Single(x => x.QuoteID == id));
        }
    }
}