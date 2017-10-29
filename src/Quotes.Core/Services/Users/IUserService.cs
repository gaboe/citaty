using MongoDB.Bson;
using Quotes.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Quotes.Core.Services.Users
{
    public interface IUserService
    {
        Task<List<User>> GetAll();

        Task<User> GetUserByUsername(string username);

        Task<User> AddUser(User user);

        Task<User> GetByID(ObjectId id);

        Task<User> GetByID(string id);

        bool Exists(ObjectId id);

        void CreateUser(User user);

        void DeleteUser(User user);

        void SetPasswordHash(ObjectId id, string passwordHash);

        void SetUsername(ObjectId id, string userName);

        void Replace(User user);
    }
}