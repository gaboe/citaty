using System.Runtime.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Quotes.Data.Domain.Models
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

        public string QuoteID => ID.ToString();
    }
}