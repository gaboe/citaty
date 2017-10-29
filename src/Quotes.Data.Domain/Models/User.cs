using Microsoft.AspNet.Identity;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace Quotes.Domain.Models
{
    public class User : IUser<ObjectId>, IEntity<ObjectId>
    {
        [BsonId]
        public ObjectId Id { get; set; }

        public string UserName { get; set; }

        public string UserID => Id.ToString();

        public DateTime DateCreated { get; set; }

        public DateTime DateUpdated { get; set; }

        public string Login { get; set; }

        public IList<Channel> FavouriteChannels { get; set; } = new List<Channel>();

        public string Password { get; set; }

        public string PasswordHash { get; set; }
    }
}