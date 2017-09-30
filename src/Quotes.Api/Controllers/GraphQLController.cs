using GraphQL;
using GraphQL.Types;
using Microsoft.AspNetCore.Mvc;
using Quotes.Api.Infrastructure;
using Quotes.GraphQL.Queries;
using System.Threading.Tasks;

namespace Quotes.Api.Controllers
{
    [Produces("application/json")]
    [Route("graphql")]
    public class GraphQLController : Controller
    {
        private readonly ISchema _schema;
        private readonly IDocumentExecuter _documentExecuter;

        public GraphQLController(
            ISchemaProvider schemaProvider,
            IDocumentExecuter documentExecuter
        )
        {
            _schema = schemaProvider.GetRootSchema();
            _documentExecuter = documentExecuter;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] GraphQLQuery query)
        {
            var executionOptions = new ExecutionOptions {Schema = _schema, Query = query.Query};
            var result = await _documentExecuter.ExecuteAsync(executionOptions).ConfigureAwait(false);

            if (result.Errors?.Count > 0)
            {
                return BadRequest(result.Errors);
            }
            return Ok(result);
        }
    }
}