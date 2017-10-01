using System.Threading.Tasks;
using Autofac;
using GraphQL;
using GraphQL.Types;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Quotes.Api.Infrastructure;
using Quotes.Data.Repositories.Quotes;
using Quotes.GraphQL;
using Quotes.GraphQL.Queries;
using Quotes.GraphQL.Types;
using Quotes.Testing.Infrastructure;

namespace Quotes.Tests.Data
{
    [TestClass]
    public class QuoteRepositoryTest
    {
        [TestMethod]
        public void GetAllQuotesTest()
        {
            using (var resolver = new TestResolver())
            {
                var quoteRepository = resolver.Resolve<IQuoteRepository>();
                var quotes = quoteRepository.GetAll().Result;

                Assert.IsNotNull(quotes);
                Assert.IsTrue(quotes.Count > 0);
            }
        }

        [TestMethod]
        public async Task DI()
        {
            using (var resolver = new TestResolver(Setup))
            {
                var schema = resolver.Resolve<ISchema>();
                var qt = resolver.Resolve<QuoteType>();
                var query = new GraphQLQuery
                {
                    Query =
                        "\n  query IntrospectionQuery {\n    __schema {\n      queryType { name }\n      mutationType { name }\n      subscriptionType { name }\n      types {\n        ...FullType\n      }\n      directives {\n        name\n        description\n        locations\n        args {\n          ...InputValue\n        }\n      }\n    }\n  }\n\n  fragment FullType on __Type {\n    kind\n    name\n    description\n    fields(includeDeprecated: true) {\n      name\n      description\n      args {\n        ...InputValue\n      }\n      type {\n        ...TypeRef\n      }\n      isDeprecated\n      deprecationReason\n    }\n    inputFields {\n      ...InputValue\n    }\n    interfaces {\n      ...TypeRef\n    }\n    enumValues(includeDeprecated: true) {\n      name\n      description\n      isDeprecated\n      deprecationReason\n    }\n    possibleTypes {\n      ...TypeRef\n    }\n  }\n\n  fragment InputValue on __InputValue {\n    name\n    description\n    type { ...TypeRef }\n    defaultValue\n  }\n\n  fragment TypeRef on __Type {\n    kind\n    name\n    ofType {\n      kind\n      name\n      ofType {\n        kind\n        name\n        ofType {\n          kind\n          name\n          ofType {\n            kind\n            name\n            ofType {\n              kind\n              name\n              ofType {\n                kind\n                name\n                ofType {\n                  kind\n                  name\n                }\n              }\n            }\n          }\n        }\n      }\n    }\n  }\n"
                };
                var documentExecuter = resolver.Resolve<IDocumentExecuter>();
                var executionOptions = new ExecutionOptions
                {
                    Schema = schema,
                    Query = query.Query
                };
                var result = await
                    documentExecuter.ExecuteAsync(executionOptions).ConfigureAwait(true);

                Assert.IsNotNull(qt);
                Assert.IsNotNull(result);
                Assert.IsTrue(result.Errors == null);
            }
        }

        private void Setup(ContainerBuilder builder)
        {
            builder.RegisterType<DocumentExecuter>().As<IDocumentExecuter>();
            builder.RegisterType<SchemaProvider>().As<ISchemaProvider>();
        }
    }
}