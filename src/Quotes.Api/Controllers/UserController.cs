using Microsoft.AspNetCore.Mvc;
using Quotes.Core.Services.Security;
using Quotes.Domain.Models;

namespace Quotes.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/user")]
    public class UserController : Controller
    {
        private readonly IIdentityService _identityService;

        public UserController(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        [HttpPost]
        public User Index([FromBody]object u)
        {
            var user = _identityService.CreateIdentity("test", "123456").Result;
            return user;
        }
    }
}