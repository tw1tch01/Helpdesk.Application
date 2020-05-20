using System;
using Helpdesk.Domain.Entities;
using Helpdesk.Domain.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Helpdesk.Persistence.Common.Configurations
{
    public class TicketConfiguration : BaseEntityConfiguration<Ticket>
    {
        public override void Configure(EntityTypeBuilder<Ticket> builder)
        {
            #region Primary Key

            builder.HasKey(ticket => ticket.TicketId);

            #endregion Primary Key

            #region Foregin Keys

            //builder.HasMany(ticket => ticket.LinkedTickets)
            //       .WithOne(ticketLink => ticketLink.FromTicket)
            //       .HasForeignKey(ticketLink => ticketLink.FromTicketId);

            //builder.HasMany(ticket => ticket.LinkedTickets)
            //       .WithOne(ticketLink => ticketLink.ToTicket)
            //       .HasForeignKey(ticketLink => ticketLink.ToTicketId);

            #endregion Foregin Keys

            #region Properties

            builder.Property(ticket => ticket.Name)
                   .IsRequired()
                   .HasMaxLength(64);

            builder.Property(ticket => ticket.Description)
                   .IsRequired();

            builder.Property(ticket => ticket.Severity)
                   .IsRequired()
                   .HasConversion(severity => severity.ToString(), severityString => (Severity)Enum.Parse(typeof(Severity), severityString));

            builder.Property(ticket => ticket.Priority)
                   .IsRequired()
                   .HasConversion(priority => priority.ToString(), priorityString => (Priority)Enum.Parse(typeof(Priority), priorityString));

            #endregion Properties

            base.Configure(builder);
        }
    }
}