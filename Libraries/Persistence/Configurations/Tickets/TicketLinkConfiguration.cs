using System;
using Helpdesk.Domain.Tickets;
using Helpdesk.Domain.Tickets.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Helpdesk.Persistence.Configurations.Tickets
{
    public class TicketLinkConfiguration : IEntityTypeConfiguration<TicketLink>
    {
        public void Configure(EntityTypeBuilder<TicketLink> builder)
        {
            #region Primary Key

            builder.HasKey(ticketLink => new
            {
                ticketLink.FromTicketId,
                ticketLink.ToTicketId
            });

            #endregion Primary Key

            #region Foreign Key

            builder.HasOne(ticketLink => ticketLink.ToTicket)
                   .WithMany(ticket => ticket.LinkedFrom)
                   .HasForeignKey(ticketLink => ticketLink.ToTicketId);

            builder.HasOne(ticketLink => ticketLink.FromTicket)
                   .WithMany(ticket => ticket.LinkedTo)
                   .HasForeignKey(ticketLink => ticketLink.FromTicketId);

            #endregion Foreign Key

            #region Properties

            builder.Property(ticketLink => ticketLink.CreatedBy)
                   .IsRequired();

            builder.Property(ticketLink => ticketLink.CreatedOn)
                   .IsRequired();

            builder.Property(ticketLink => ticketLink.CreatedProcess)
                   .IsRequired()
                   .HasMaxLength(1024);

            builder.Property(ticketLink => ticketLink.LinkType)
                   .IsRequired()
                   .HasMaxLength(32)
                   .HasConversion(linkType => linkType.ToString(), linkType => (TicketLinkType)Enum.Parse(typeof(TicketLinkType), linkType));

            #endregion Properties
        }
    }
}