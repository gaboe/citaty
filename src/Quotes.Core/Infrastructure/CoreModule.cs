using Autofac;
using Quotes.Core.Services.Channels;
using Quotes.Core.Services.Quotes;
using Quotes.Core.Services.Security;
using Quotes.Core.Services.Users;

namespace Quotes.Core.Infrastructure
{
    public class CoreModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<IdentityService>().As<IIdentityService>();

            builder.RegisterType<QuoteService>().As<IQuoteService>();
            builder.RegisterType<ChannelService>().As<IChannelService>();
            builder.RegisterType<UserService>().As<IUserService>();
        }
    }
}