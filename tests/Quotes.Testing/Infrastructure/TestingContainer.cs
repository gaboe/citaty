using Autofac;
using Microsoft.Extensions.Configuration;
using Moq;
using Quotes.Core.Infrastructure;
using Quotes.Core.Services.Security;
using Quotes.Data.Infrastructure;
using Quotes.Domain.Settings;
using Quotes.GraphQL.Infrastructure;
using Quotes.Testing.Providers;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace Quotes.Testing.Infrastructure
{
    public class TestingContainer : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var configurationRoot = AppSettingsProvider.GetConfigurationRoot();
            var appConfig = configurationRoot.GetSection("App").Get<AppSettings>();

            builder.RegisterModule(new CoreModule());
            builder.RegisterModule(new DataModule(appConfig));
            builder.RegisterModule(new GraphQLModule());
            builder.RegisterModule(new TestingModule());

            var identityService = new Mock<IIdentityService>();
            identityService.Setup(x => x.GetIdentity(TestingConstants.UserName,
                    TestingConstants.UserPassword))
                .Returns(
                    Task.FromResult(
                        new ClaimsIdentity(
                            new GenericIdentity(TestingConstants.UserName, "Token"),
                            new Claim[] { }
                        )
                    ));

            builder.RegisterInstance(identityService.Object);
        }
    }
}