using Autofac;
using Quotes.GraphQL.Queries;
using Quotes.GraphQL.Types;

namespace Quotes.GraphQL.Infrastructure
{
    public class GraphQLModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<RootQuery>();
            builder.RegisterType<QuoteType>();
            builder.RegisterType<ChannelType>();
        }
    }
}