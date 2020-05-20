using Helpdesk.Application.Tickets;
using Helpdesk.Services.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace Helpdesk.Application.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddServices();

            #region Application Services

            services.AddTransient<TicketCommandService>();
            services.AddTransient<TicketActionService>();
            services.AddTransient<TicketQueryService>();

            #endregion Application Services

            return services;
        }
    }
}