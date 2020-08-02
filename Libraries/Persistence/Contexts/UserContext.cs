using Data.Common;
using Helpdesk.Domain.Users;
using Helpdesk.Persistence.Configurations.Users;
using Helpdesk.Services.Common.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Helpdesk.Persistence.Contexts
{
    public class UserContext : DbContext, IUserContext
    {
        private readonly ILogger<UserContext> _logger;

        public UserContext(DbContextOptions<UserContext> options, ContextScope scope, ILoggerFactory loggerFactory)
            : base(options)
        {
            ContextScope = scope;
            _logger = loggerFactory.CreateLogger<UserContext>();
        }

        public ContextScope ContextScope { get; }
        public DbSet<User> Users { get; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new UserConfiguration());
        }
    }
}