using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Quotes.Domain.Models
{
    public class Quote : IEntity<ObjectId>
    {
        [BsonId]
        public ObjectId ID { get; set; }

        public string QuoteID => ID.ToString();

        public DateTime DateCreated { get; set; }

        public DateTime DateUpdated { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public ObjectId ChannelID { get; set; }

        public string GetChannelID => ChannelID.ToString();

    }
}