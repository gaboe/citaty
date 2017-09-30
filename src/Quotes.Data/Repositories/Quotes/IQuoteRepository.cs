﻿using MongoDB.Bson;
using Quotes.Domain.Models;

namespace Quotes.Data.Repositories.Quotes
{
    public interface IQuoteRepository : IBaseRepository<Quote, ObjectId> { }
}