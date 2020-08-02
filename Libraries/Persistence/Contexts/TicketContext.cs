using Data.Common;
using Helpdesk.Domain.Tickets;
using Helpdesk.Persistence.Configurations.Tickets;
using Helpdesk.Services.Common.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Helpdesk.Persistence.Contexts
{
    public class TicketContext : DbContext, ITicketContext
    {
        private readonly ILogger<TicketContext> _logger;

        public TicketContext(DbContextOptions<TicketContext> options, ContextScope scope, ILoggerFactory loggerFactory)
            : base(options)
        {
            ContextScope = scope;
            _logger = loggerFactory.CreateLogger<TicketContext>();
        }

        public ContextScope ContextScope { get; }
        public DbSet<Ticket> Tickets { get; }
        public DbSet<TicketLink> TicketLinks { get; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new TicketConfiguration());
            modelBuilder.ApplyConfiguration(new TicketLinkConfiguration());
        }
    }
}