using Autofac;
using GraphQL;
using GraphQL.Types;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Quotes.GraphQL.Queries;
using Quotes.Testing.Infrastructure;
using System.Linq;
using System.Threading.Tasks;

namespace Quotes.Tests.GraphQL.Queries
{
    [TestClass]
    public class IntrospectionQueryTest
    {
        [TestMethod]
        public async Task IntrospectionTest()
        {
            using (var resolver = new TestResolver(Setup))
            {
                var schema = resolver.Resolve<ISchema>();
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

                Assert.IsNotNull(result);
                Assert.IsTrue(result.Errors == null,
                    string.Concat(result.Errors != null && result.Errors.Any()
                        ? result.Errors
                        : new ExecutionErrors()));
            }
        }

        private static void Setup(ContainerBuilder builder)
        {
            builder.RegisterType<DocumentExecuter>().As<IDocumentExecuter>();
        }
    }
}