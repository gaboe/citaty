using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace Quotes.Domain.Models
{
    public class Channel : IEntity<ObjectId>
    {
        [BsonId]
        public ObjectId Id { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateUpdated { get; set; }

        public string Title { get; set; }

        public IEnumerable<Quote> Quotes { get; set; }

        public string ChannelID => Id.ToString();
    }
}