using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Helpdesk.Persistence.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    TicketId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Identifier = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(nullable: false),
                    CreatedProcess = table.Column<string>(maxLength: 1024, nullable: false),
                    ModifiedBy = table.Column<string>(nullable: true),
                    ModifiedOn = table.Column<DateTimeOffset>(nullable: true),
                    ModifiedProcess = table.Column<string>(maxLength: 1024, nullable: true),
                    UserGuid = table.Column<Guid>(nullable: false),
                    AssignedUserGuid = table.Column<Guid>(nullable: true),
                    Name = table.Column<string>(maxLength: 64, nullable: false),
                    Description = table.Column<string>(nullable: false),
                    DueDate = table.Column<DateTimeOffset>(nullable: true),
                    Severity = table.Column<string>(maxLength: 32, nullable: false),
                    Priority = table.Column<string>(maxLength: 32, nullable: false),
                    AssignedOn = table.Column<DateTimeOffset>(nullable: true),
                    StartedOn = table.Column<DateTimeOffset>(nullable: true),
                    StartedBy = table.Column<Guid>(nullable: true),
                    PausedOn = table.Column<DateTimeOffset>(nullable: true),
                    PausedBy = table.Column<Guid>(nullable: true),
                    ResolvedOn = table.Column<DateTimeOffset>(nullable: true),
                    ResolvedBy = table.Column<Guid>(nullable: true),
                    ClosedOn = table.Column<DateTimeOffset>(nullable: true),
                    ClosedBy = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.TicketId);
                });

            migrationBuilder.CreateTable(
                name: "TicketLinks",
                columns: table => new
                {
                    FromTicketId = table.Column<int>(nullable: false),
                    ToTicketId = table.Column<int>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(nullable: false),
                    CreatedProcess = table.Column<string>(maxLength: 1024, nullable: false),
                    LinkType = table.Column<string>(maxLength: 32, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketLinks", x => new { x.FromTicketId, x.ToTicketId });
                    table.ForeignKey(
                        name: "FK_TicketLinks_Tickets_FromTicketId",
                        column: x => x.FromTicketId,
                        principalTable: "Tickets",
                        principalColumn: "TicketId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TicketLinks_Tickets_ToTicketId",
                        column: x => x.ToTicketId,
                        principalTable: "Tickets",
                        principalColumn: "TicketId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TicketLinks_ToTicketId",
                table: "TicketLinks",
                column: "ToTicketId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TicketLinks");

            migrationBuilder.DropTable(
                name: "Tickets");
        }
    }
}
