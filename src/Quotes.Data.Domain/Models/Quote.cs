using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Quotes.Data.Domain;
using System.Runtime.Serialization;

namespace Quotes.Domain.Models
{
    [DataContract]
    public class Quote : IEntity<ObjectId>
    {
        [DataMember]
        [BsonId]
        public ObjectId ID { get; set; }

        public string QuoteID => ID.ToString();

        [DataMember]
        [BsonElement("title")]
        public string Title { get; set; }

        [DataMember]
        [BsonElement("content")]
        public string Content { get; set; }

        [DataMember]
        [BsonElement("channelID")]
        public ObjectId ChannelID { get; set; }
    }
}