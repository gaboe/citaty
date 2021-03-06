﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Quotes.Data.Repositories.Channels;
using Quotes.Data.Repositories.Users;
using Quotes.Domain.Models;
using Quotes.Testing.Core;
using Quotes.Testing.Core.Infrastructure;
using System;
using System.Linq;

namespace Quotes.Tests.Data
{
    [TestClass]
    public class UserRespositoryTest
    {
        [TestMethod]
        public void InsertUserTest()
        {
            using (var resolver = new TestResolver())
            {
                //Arrange
                var userRepository = resolver.Resolve<IUserRepository>();
                var userName = $"Integration{Guid.NewGuid()}";
                var user = new User
                {
                    UserName = userName,
                };

                //Action
                userRepository.Add(user);
                var user2 = userRepository.Get(user.Id).Result;

                //Assert
                Assert.IsNotNull(user.Id);
                Assert.IsNotNull(user.DateCreated);
                Assert.IsNotNull(user.DateUpdated);
                Assert.AreEqual(userName, user2.UserName);
                Assert.IsNotNull(user2.DateCreated);
                Assert.IsNotNull(user2.DateUpdated);
            }
        }

        [TestMethod]
        public void GetUserByLoginTest()
        {
            using (var resolver = new TestResolver())
            {
                //Arrange
                var userRepository = resolver.Resolve<IUserRepository>();
                var channelRepository = resolver.Resolve<IChannelRepository>();

                //Action
                var user = userRepository.GetUserByLogin(TestingConstants.UserName).Result;
                var channels = channelRepository.GetMany(user.FavouriteChannels.Select(x => x.Id)).Result;

                //Assert
                Assert.IsNotNull(user.Id);
                Assert.IsNotNull(user.DateCreated);
                Assert.IsNotNull(user.DateUpdated);
                Assert.IsNotNull(user.FavouriteChannels);
                Assert.IsTrue(user.FavouriteChannels.Count > 0);
                Assert.AreEqual(user.FavouriteChannels.Count, channels.Count);
            }
        }
    }
}