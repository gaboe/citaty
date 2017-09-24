using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Citaty.Data.Queries;
using GraphQL;
using GraphQL.Types;

namespace Citaty.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Quotes")]
    public class QuotesController : Controller
    {
        private readonly QuoteQuery _quoteQueries;

        public QuotesController(QuoteQuery quoteQueries)
        {
            _quoteQueries = quoteQueries;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] GraphQLQuery query)
        {
            var schema = new Schema { Query = _quoteQueries };

            var result = await new DocumentExecuter().ExecuteAsync(_ =>
            {
                _.Schema = schema;
                _.Query = query.Query;

            }).ConfigureAwait(false);

            if (result.Errors?.Count > 0)
            {
                return BadRequest();
            }

            return Ok(result);
        }
    }
}