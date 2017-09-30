using System.Runtime.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Quotes.Data.Domain;

namespace Quotes.Domain.Models
{
    [DataContract]
    public class Quote : IEntity<ObjectId>
    {
        [DataMember]
        [BsonId]
        public ObjectId ID { get; set; }

        [DataMember]
        [BsonElement("title")]
        public string Title { get; set; }

        [DataMember]
        [BsonElement("content")]
        public string Content { get; set; }

        public string QuoteID => ID.ToString();
    }
}