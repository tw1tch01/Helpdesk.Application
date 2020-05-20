using Microsoft.EntityFrameworkCore.Migrations;

namespace Helpdesk.Persistence.MySql.Migrations
{
    public partial class TicketEnumsToStringConvertions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Severity",
                table: "Tickets",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Priority",
                table: "Tickets",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Severity",
                table: "Tickets",
                type: "int",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<int>(
                name: "Priority",
                table: "Tickets",
                type: "int",
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
