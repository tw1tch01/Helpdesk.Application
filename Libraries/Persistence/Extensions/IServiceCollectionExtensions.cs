using System;
using Data.Contexts;
using Helpdesk.Persistence.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace Helpdesk.Persistence.Extensions
{
    public static class IServiceCollectionExtensions
    {
        //public static IServiceCollection AddTicketContext(this IServiceCollection services, Action<MySqlOptions> mySqlOptions)
        //{
        //    var options = new MySqlOptions();
        //    mySqlOptions(options);

        //    services.AddTransient<ITicketContext, TicketContext>();
        //    services.ConfigureMySqlContext<TicketContext>(options);

        //    return services;
        //}

        //public static IServiceCollection AddUserContext(this IServiceCollection services, Action<MySqlOptions> mySqlOptions)
        //{
        //    var options = new MySqlOptions();
        //    mySqlOptions(options);

        //    services.AddTransient<IUserContext, UserContext>();
        //    services.ConfigureMySqlContext<UserContext>(options);

        //    return services;
        //}

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