using Data.Common;
using Helpdesk.Domain.Entities;
using Helpdesk.Persistence.Common.Configurations;
using Helpdesk.Services.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Helpdesk.Persistence.Common.Contexts
{
    public class TicketContext : DbContext, ITicketContext
    {
        private readonly ILogger<TicketContext> _logger;

        public TicketContext(DbContextOptions options, ContextScope scope, ILoggerFactory loggerFactory)
            : base(options)
        {
            ContextScope = scope;
            _logger = loggerFactory.CreateLogger<TicketContext>();
        }

        public ContextScope ContextScope { get; }

        #region DbSets

        public DbSet<Ticket> Tickets { get; }

        #endregion DbSets

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new TicketConfiguration());
        }
    }
}