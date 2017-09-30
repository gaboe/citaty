﻿using System.Collections.Generic;
using System.Runtime.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Quotes.Data.Domain;

namespace Quotes.Domain.Models
{
    [DataContract]
    public class Channel : IEntity<ObjectId>
    {
        [DataMember]
        [BsonId]
        public ObjectId ID { get; set; }

        [DataMember]
        [BsonElement("title")]
        public string Title { get; set; }

        [DataMember]
        [BsonElement("quotes")]
        public IEnumerable<Quote> Quotes { get; set; }

        public string ChannelID => ID.ToString();
    }
}