using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Quotes.Api.Controllers
{
    public class GraphQLController : Controller
    {
        [Route("api/graphql")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
