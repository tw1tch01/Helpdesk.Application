using System;
using System.Linq;
using System.Threading.Tasks;
using Data.Common;
using Helpdesk.Domain.Users;
using Helpdesk.Infrastructure.Identity;
using Helpdesk.Persistence.Configurations.Users;
using Helpdesk.Services.Common.Contexts;
using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Helpdesk.Persistence.Contexts
{
    public class UserContext : ApiAuthorizationDbContext<ApplicationUser>, IUserContext
    {
        private readonly ILogger<UserContext> _logger;

        public UserContext(
            DbContextOptions<UserContext> options,
            IOptions<OperationalStoreOptions> operationalStoreOptions,
            ContextScope scope,
            ILoggerFactory loggerFactory) : base(options, operationalStoreOptions)
        {
            ContextScope = scope;
            _logger = loggerFactory.CreateLogger<UserContext>();
        }

        public ContextScope ContextScope { get; }
        DbSet<User> IUserContext.Users { get; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new UserConfiguration());
        }

        public static async Task SeedDefaultUsersAsync(UserManager<ApplicationUser> userManager)
        {
            var defaultUser = new ApplicationUser 
            { 
                UserName = "administrator@localhost", 
                Email = "administrator@localhost" 
            };

            if (userManager.Users.All(u => u.UserName != defaultUser.UserName))
            {
                defaultUser.UserGuid = Guid.NewGuid();
                await userManager.CreateAsync(defaultUser, "P@55w0rd");
            }
        }
    }
}