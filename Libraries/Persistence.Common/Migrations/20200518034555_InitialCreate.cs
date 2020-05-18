using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Helpdesk.Persistence.Common.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    TicketId = table.Column<int>(nullable: false),
                    CreatedBy = table.Column<int>(nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(nullable: false),
                    CreatedProcess = table.Column<string>(maxLength: 1024, nullable: false),
                    ModifiedBy = table.Column<int>(nullable: true),
                    ModifiedOn = table.Column<DateTimeOffset>(nullable: true),
                    ModifiedProcess = table.Column<string>(maxLength: 1024, nullable: true),
                    Identifier = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 64, nullable: false),
                    Description = table.Column<string>(nullable: false),
                    DueDate = table.Column<DateTimeOffset>(nullable: true),
                    Severity = table.Column<int>(nullable: false),
                    Priority = table.Column<int>(nullable: false),
                    StartedOn = table.Column<DateTimeOffset>(nullable: true),
                    PausedOn = table.Column<DateTimeOffset>(nullable: true),
                    ResolvedOn = table.Column<DateTimeOffset>(nullable: true),
                    ResolvedBy = table.Column<int>(nullable: true),
                    ClosedOn = table.Column<DateTimeOffset>(nullable: true),
                    ClosedBy = table.Column<int>(nullable: true),
                    ClientId = table.Column<int>(nullable: false),
                    ProjectId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.TicketId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tickets");
        }
    }
}