using Quotes.Data.Repositories.Users;
using Quotes.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;

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

        public Task<User> GetUserByLogin(string login)
        {
            return _userRepository.GetUserByLogin(login);
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

        public void SetPasswordHash(ObjectId id, string passwordHash)
        {
            _userRepository.SetPasswordHash(id, passwordHash);
        }
    }
}