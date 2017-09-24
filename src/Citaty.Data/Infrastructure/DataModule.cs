using Autofac;
using Citaty.Data.Queries;
using Citaty.Data.Repositories.Quotes;

namespace Citaty.Data.Infrastructure
{
    public class DataModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<QuoteRepository>().As<IQuoteRepository>();
            builder.RegisterType<QuoteQuery>();
        }
    }
}