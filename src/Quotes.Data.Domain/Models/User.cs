using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace Quotes.Domain.Models
{
    public class User : IEntity<ObjectId>
    {
        [BsonId]
        public ObjectId ID { get; set; }

        public string UserID => ID.ToString();

        public DateTime DateCreated { get; set; }

        public DateTime DateUpdated { get; set; }

        public string Login { get; set; }

        public IList<Channel> FavouriteChannels { get; set; }
    }
}