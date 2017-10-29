using Quotes.Data.Repositories.Users;
using Quotes.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Quotes.Core.Services.Users
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Task<List<User>> GetAll()
        {
            return _userRepository.GetAll();
        }

        public Task<User> GetUserByUsername(string username)
        {
            return _userRepository.GetUserByLogin(username);
        }

        public Task<User> AddUser(User user)
        {
            return Task.FromResult(_userRepository.Add(user));
        }

        public Task<User> GetByID(ObjectId id)
        {
            return _userRepository.Get(id);
        }

        public bool Exists(ObjectId id)
        {
            return _userRepository.Exists(id).Result;
        }

        public void CreateUser(User user)
        {
            _userRepository.Add(user);
        }

        public void DeleteUser(User user)
        {
            _userRepository.Delete(user.Id);
        }

        public Task<User> GetByID(string id)
        {
            return _userRepository.Get(id);
        }

        public void SetPasswordHash(ObjectId id, string passwordHash)
        {
            _userRepository.SetPasswordHash(id, passwordHash);
        }

        public void SetUsername(ObjectId id, string userName)
        {
            _userRepository.SetUsername(id, userName);
        }

        public void Replace(User user)
        {
            _userRepository.Replace(user);
        }
    }
}