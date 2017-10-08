using Autofac;
using GraphQL.Types;
using Quotes.GraphQL.Queries;
using Quotes.GraphQL.Types;
using System;
using Quotes.GraphQL.Schemas;

namespace Quotes.GraphQL.Infrastructure
{
    public class GraphQLModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<RootQuery>().AsSelf();
            builder.RegisterType<QuoteType>().AsSelf();
            builder.RegisterType<ChannelType>().AsSelf();
            builder.RegisterType<UserType>().AsSelf();

            builder.RegisterType<QuotesSchema>().As<ISchema>();

            builder.Register<Func<Type, GraphType>>(c =>
            {
                var context = c.Resolve<IComponentContext>();
                return t =>
                {
                    var resolved = context.Resolve(t);
                    return (GraphType) resolved;
                };
            });
        }
    }
}