﻿// <auto-generated />
using System;
using Helpdesk.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Helpdesk.Persistence.Migrations.Tickets
{
    [DbContext(typeof(TicketContext))]
    partial class TicketContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Helpdesk.Domain.Tickets.Ticket", b =>
                {
                    b.Property<int>("TicketId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTimeOffset?>("AssignedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid?>("AssignedUserGuid")
                        .HasColumnType("char(36)");

                    b.Property<Guid?>("ClosedBy")
                        .HasColumnType("char(36)");

                    b.Property<DateTimeOffset?>("ClosedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTimeOffset>("CreatedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("CreatedProcess")
                        .IsRequired()
                        .HasColumnType("varchar(1024) CHARACTER SET utf8mb4")
                        .HasMaxLength(1024);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTimeOffset?>("DueDate")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("Identifier")
                        .HasColumnType("char(36)");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTimeOffset?>("ModifiedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("ModifiedProcess")
                        .HasColumnType("varchar(1024) CHARACTER SET utf8mb4")
                        .HasMaxLength(1024);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(64) CHARACTER SET utf8mb4")
                        .HasMaxLength(64);

                    b.Property<Guid?>("PausedBy")
                        .HasColumnType("char(36)");

                    b.Property<DateTimeOffset?>("PausedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Priority")
                        .IsRequired()
                        .HasColumnType("varchar(32) CHARACTER SET utf8mb4")
                        .HasMaxLength(32);

                    b.Property<Guid?>("ResolvedBy")
                        .HasColumnType("char(36)");

                    b.Property<DateTimeOffset?>("ResolvedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Severity")
                        .IsRequired()
                        .HasColumnType("varchar(32) CHARACTER SET utf8mb4")
                        .HasMaxLength(32);

                    b.Property<Guid?>("StartedBy")
                        .HasColumnType("char(36)");

                    b.Property<DateTimeOffset?>("StartedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("UserGuid")
                        .HasColumnType("char(36)");

                    b.HasKey("TicketId");

                    b.ToTable("Tickets");
                });

            modelBuilder.Entity("Helpdesk.Domain.Tickets.TicketLink", b =>
                {
                    b.Property<int>("FromTicketId")
                        .HasColumnType("int");

                    b.Property<int>("ToTicketId")
                        .HasColumnType("int");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTimeOffset>("CreatedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("CreatedProcess")
                        .IsRequired()
                        .HasColumnType("varchar(1024) CHARACTER SET utf8mb4")
                        .HasMaxLength(1024);

                    b.Property<string>("LinkType")
                        .IsRequired()
                        .HasColumnType("varchar(32) CHARACTER SET utf8mb4")
                        .HasMaxLength(32);

                    b.HasKey("FromTicketId", "ToTicketId");

                    b.HasIndex("ToTicketId");

                    b.ToTable("TicketLinks");
                });

            modelBuilder.Entity("Helpdesk.Domain.Tickets.TicketLink", b =>
                {
                    b.HasOne("Helpdesk.Domain.Tickets.Ticket", "FromTicket")
                        .WithMany("LinkedTo")
                        .HasForeignKey("FromTicketId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Helpdesk.Domain.Tickets.Ticket", "ToTicket")
                        .WithMany("LinkedFrom")
                        .HasForeignKey("ToTicketId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
