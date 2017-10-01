using Autofac;
using Quotes.Core.Services.Quotes;

namespace Quotes.Core.Infrastructure
{
    public class CoreModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<QuoteService>().As<IQuoteService>();
        }
    }
}