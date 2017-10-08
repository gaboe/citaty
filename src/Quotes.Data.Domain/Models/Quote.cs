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

        public string Content { get; set; }

        public ObjectId ChannelID { get; set; }

        public string OwningChannelID
        {
            get => ChannelID.ToString();
            set
            {
                if (value == null) throw new ArgumentNullException(nameof(value));
                ChannelID = ObjectId.Parse(value);
            }
        }

        public Channel Channel { get; set; }
    }
}