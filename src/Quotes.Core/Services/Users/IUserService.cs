using Quotes.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace Quotes.Core.Services.Users
{
    public interface IUserService
    {
        Task<List<User>> GetAll();

        Task<User> GetUserByLogin(string login);

        Task<User> AddUser(User user);
        Task<User> GetByID(ObjectId id);
        void CreateUser(User user);

        void DeleteUser(User user);
        Task<User> GetByID(string id);
    }
}