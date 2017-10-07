using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace Quotes.Domain.Models
{
    //[DataContract]
    public class User : IEntity<ObjectId>
    {
        //[DataMember]
        [BsonId]
        public ObjectId ID { get; set; }

        public string UserID => ID.ToString();

        //[DataMember]
        //[BsonElement("dateCreated")]
        public DateTime DateCreated { get; set; }


        //[DataMember]
        //[BsonElement("dateUpdated")]
        public DateTime DateUpdated { get; set; }

        //[DataMember]
        //[BsonElement("login")]
        public string Login { get; set; }

        public IList<Channel> FavouriteChannels { get; set; }
    }
}