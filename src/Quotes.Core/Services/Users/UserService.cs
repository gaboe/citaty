using MongoDB.Bson;
using Quotes.Data.Repositories.Users;
using Quotes.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        public Task<User> GetUserByNormalizedUsername(string normalizedUserName)
        {
            return _userRepository.GetUserByNormalizedUsername(normalizedUserName);
        }

        public Task<User> AddUser(User user)
        {
            return Task.FromResult(_userRepository.Add(user));
        }

        public Task<User> GetByID(ObjectId id)
        {
            return _userRepository.Get(id);
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

        public void Replace(User user)
        {
            _userRepository.Replace(user);
        }
    }
}