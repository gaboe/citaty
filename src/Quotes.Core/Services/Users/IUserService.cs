using Quotes.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Quotes.Core.Services.Users
{
    public interface IUserService
    {
        Task<List<User>> GetAll();

        Task<User> GetUserByLogin(string login);

        Task<User> AddUser(User user);
    }
}