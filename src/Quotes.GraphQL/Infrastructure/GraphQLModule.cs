using Autofac;
using Quotes.GraphQL.Queries;

namespace Quotes.GraphQL.Infrastructure
{
    public class GraphQLModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<RootQuery>();
        }
    }
}