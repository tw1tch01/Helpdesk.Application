using System.Reflection;
using Helpdesk.Services.Extensions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Helpdesk.Application.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddServices();

            services.AddMediatR(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}