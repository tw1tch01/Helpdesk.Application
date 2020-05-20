using System;
using Helpdesk.Persistence.Common.Contexts;
using Helpdesk.Persistence.SqlServer.Options;
using Helpdesk.Services.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Helpdesk.Persistence.SqlServer.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSqlServerPersistence(this IServiceCollection services, Action<SqlServerOptions> sqlServerOptions)
        {
            var options = new SqlServerOptions();
            sqlServerOptions(options);

            services.AddTransient<ITicketContext, TicketContext>();

            services.AddDbContext<TicketContext>(opt => opt
                .UseSqlServer(options.ConnectionString, contextOptions =>
                {
                    contextOptions.MigrationsAssembly("Helpdesk.Persistence.SqlServer");
                }), ServiceLifetime.Transient);

            return services;
        }
    }
}