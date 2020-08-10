using System;
using Data.Common;
using Data.Contexts;
using Helpdesk.Persistence.Actions;
using Helpdesk.Persistence.Common.Options;
using Helpdesk.Persistence.Contexts;
using Helpdesk.Persistence.Options;
using Helpdesk.Services.Common.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace Helpdesk.Persistence.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDataServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddScoped(provider =>
            {
                var addedContextOptions = provider.GetService<IConfigureOptions<AddedStateAction>>();
                var addedContext = new AddedStateAction();
                addedContextOptions.Configure(addedContext);

                var modifiedContextOptions = provider.GetService<IConfigureOptions<ModifiedStateAction>>();
                var modifiedContext = new ModifiedStateAction();
                modifiedContextOptions.Configure(modifiedContext);

                var contextScope = new ContextScope();
                contextScope.StateActions[EntityState.Added] = addedContext.SetCreatedAuditFields;
                contextScope.StateActions[EntityState.Modified] = modifiedContext.SetModifiedAuditFields;
                return contextScope;
            });

            var contextOptions = new ContextOptions();
            configuration.GetSection(ConfigurationSections.Contexts).Bind(contextOptions);

            services.ConfigureMySqlContext<ITicketContext, TicketContext>(contextOptions.TicketContext);

            return services;
        }

        public static IServiceCollection ConfigureMySqlContext<IContext, TContext>
        (
            this IServiceCollection services,
            MySqlOptions options
        ) where IContext : IAuditedContext where TContext : DbContext, IContext
        {
            services.AddTransient(typeof(IContext), typeof(TContext));

            services.AddDbContext<TContext>(opt => opt
                    .UseMySql($"Server={options.Server};Database={options.Database};User={options.Username};Password={options.Password};",
                        contextOptions =>
                        {
                            contextOptions.ServerVersion(new Version(options.Version.Major, options.Version.Minor, options.Version.Build), ServerType.MySql);
                        }), ServiceLifetime.Transient);

            return services;
        }
    }
}