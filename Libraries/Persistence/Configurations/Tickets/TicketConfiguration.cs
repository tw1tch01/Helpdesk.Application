using System;
using Helpdesk.Domain.Tickets;
using Helpdesk.Domain.Tickets.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Helpdesk.Persistence.Configurations.Tickets
{
    public class TicketConfiguration : BaseEntityConfiguration<Ticket>
    {
        public override void Configure(EntityTypeBuilder<Ticket> builder)
        {
            #region Primary Key

            builder.HasKey(ticket => ticket.TicketId);

            #endregion Primary Key

            #region Properties

            builder.Property(ticket => ticket.Name)
                   .IsRequired()
                   .HasMaxLength(64);

            builder.Property(ticket => ticket.Description)
                   .IsRequired();

            builder.Property(ticket => ticket.Severity)
                   .IsRequired()
                   .HasMaxLength(32)
                   .HasConversion(severity => severity.ToString(), severityString => (Severity)Enum.Parse(typeof(Severity), severityString));

            builder.Property(ticket => ticket.Priority)
                   .IsRequired()
                   .HasMaxLength(32)
                   .HasConversion(priority => priority.ToString(), priorityString => (Priority)Enum.Parse(typeof(Priority), priorityString));

            #endregion Properties

            base.Configure(builder);
        }
    }
}