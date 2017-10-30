using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Quotes.Api.Infrastructure;
using Quotes.Core.Middlewares.Security;
using Quotes.Domain.Settings;
using System;
using Microsoft.AspNetCore.Identity;
using Quotes.Core.Services.Security;
using Quotes.Domain.Models;

namespace Quotes.Api
{
    public partial class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            ConfigureTokens();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            ConfigureAuth(services);

            services.AddTransient<IUserStore<User>, UserStore>();
            services.AddTransient<IRoleStore<IdentityRole>, RoleStore>();
            services.AddIdentity<User,IdentityRole>().AddDefaultTokenProviders();
            var builder = new ContainerBuilder();
            var appConfig = Configuration.GetSection("App").Get<AppSettings>();
            builder.RegisterModule(new WebModule(appConfig));
            builder.Populate(services);

            var container = builder.Build();
            //Create the IServiceProvider based on the container.
            return new AutofacServiceProvider(container);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseGraphiQl();

            app.UseTokenProvider(_tokenProviderOptions);
            app.UseAuthentication();
            app.UseMvc();
            app.UseDefaultFiles();
        }
    }
}