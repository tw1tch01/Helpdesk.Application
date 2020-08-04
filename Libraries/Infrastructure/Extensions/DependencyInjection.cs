using Helpdesk.Common.Configurations;
using Helpdesk.Common.Interfaces;
using Helpdesk.Infrastructure.Identity;
using Helpdesk.Persistence.Contexts;
using Helpdesk.Persistence.Extensions;
using Helpdesk.Services.Common.Contexts;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Helpdesk.Infrastructure.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var contextOptions = new ContextOptions();
            configuration.GetSection(ConfigurationSections.Contexts).Bind(contextOptions);
            services.ConfigureMySqlContext<IUserContext, UserContext>(contextOptions.UserContext);

            services.AddDefaultIdentity<ApplicationUser>()
                    .AddEntityFrameworkStores<UserContext>();

            services.AddIdentityServer()
                    .AddApiAuthorization<ApplicationUser, UserContext>();

            services.AddTransient<IIdentityService, IdentityService>();

            services.AddAuthentication().AddIdentityServerJwt();

            return services;
        }
    }
}