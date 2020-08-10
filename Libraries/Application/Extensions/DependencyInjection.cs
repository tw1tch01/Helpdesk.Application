using Helpdesk.Services.Common;
using Helpdesk.Services.Extensions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Helpdesk.Application.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddServices();

            services.AddMediatR(typeof(DependencyInjection).Assembly);
            return services;
        }
    }
}