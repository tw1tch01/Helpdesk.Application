using System;
using Helpdesk.Persistence.Common.Contexts;
using Helpdesk.Persistence.MySql.Options;
using Helpdesk.Services.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace Helpdesk.Persistence.MySql.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMySqlPersistence(this IServiceCollection services, Action<MySqlOptions> mySqlOptions)
        {
            var options = new MySqlOptions();
            mySqlOptions(options);

            services.AddTransient<ITicketContext, TicketContext>();

            services.AddDbContext<TicketContext>(opt => opt
                .UseMySql($"Server={options.Server};Database={options.Database};User={options.Username};Password={options.Password};",
                contextOptions =>
                {
                    contextOptions.ServerVersion(new Version(options.Version.Major, options.Version.Minor, options.Version.Build), ServerType.MySql);
                    contextOptions.MigrationsAssembly("Helpdesk.Persistence.Common");
                }), ServiceLifetime.Transient);

            return services;
        }
    }
}