using System.Runtime.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Quotes.Data.Domain.Models
{
    [DataContract]
    public class Quote
    {
        [DataMember]
        [BsonId]
        public ObjectId QuoteID { get; set; }

        [DataMember]
        [BsonElement("title")]
        public string Title { get; set; }

        public string Id => QuoteID.ToString();
    }
}