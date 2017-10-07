﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Quotes.Data.Repositories.Users;
using Quotes.Domain.Models;
using Quotes.Testing;
using Quotes.Testing.Infrastructure;
using System;

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
                var login = $"Integration{Guid.NewGuid()}";
                var user = new User
                {
                    Login = login,
                };

                //Action
                userRepository.Add(user);
                var user2 = userRepository.Get(user.ID).Result;

                //Assert
                Assert.IsNotNull(user.ID);
                Assert.IsNotNull(user.DateCreated);
                Assert.IsNotNull(user.DateUpdated);
                Assert.AreEqual(login, user2.Login);
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

                //Action
                var user = userRepository.GetUserByLogin(TestingConstants.UserLogin).Result;

                //Assert
                Assert.IsNotNull(user.ID);
                Assert.IsNotNull(user.DateCreated);
                Assert.IsNotNull(user.DateUpdated);
                Assert.IsNotNull(user.FavouriteChannels);
                Assert.IsTrue(user.FavouriteChannels.Count > 0);
            }
        }
    }
}