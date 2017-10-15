using Autofac;
using GraphQL.Types;
using Quotes.GraphQL.Mutations;
using Quotes.GraphQL.Parsers;
using Quotes.GraphQL.Queries;
using Quotes.GraphQL.Schemas;
using Quotes.GraphQL.Types;
using System;
using Quotes.GraphQL.Creators;

namespace Quotes.GraphQL.Infrastructure
{
    public class GraphQLModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<QuotesSchema>().As<ISchema>();
            builder.RegisterType<RootQuery>().AsSelf();
            builder.RegisterType<RootMutation>().AsSelf();

            builder.RegisterType<ChannelType>().AsSelf();
            builder.RegisterType<QuoteType>().AsSelf();
            builder.RegisterType<UserType>().AsSelf();

            builder.RegisterType<UserInputType>().AsSelf();
            builder.RegisterType<QuoteInputType>().AsSelf();

            builder.Register<Func<Type, GraphType>>(c =>
            {
                var context = c.Resolve<IComponentContext>();
                return t =>
                {
                    var resolved = context.Resolve(t);
                    return (GraphType)resolved;
                };
            });

            builder.RegisterType<Parsers.GraphQLParser>().As<IGraphQLParser>();
            builder.RegisterType<QueryCreator>().As<IQueryCreator>();
        }
    }
}